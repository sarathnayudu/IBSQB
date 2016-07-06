﻿using System.Configuration;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using IntuitSampleMVC.utils;
using IntuitSampleMVC.Business;
using IntuitSampleMVC.Models;
using WebMatrix.WebData;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// This flow enables single sign on (SSO) between this app and the Intuit App Center.
    /// This feature offers two key benefits:  First, the user only has to sign in once, 
    /// instead of having to sign in to both this app and Intuit App Center.  
    /// Second, this app does not need to implement a customized solution for user authentication 
    /// because it relies on Intuit's OpenID service.
    /// the following occurs during the sign in process:
    /// 1.The user initiates the sign in process by going to your app and clicking the Sign in with Intuit button.
    /// 2.The Intuit sign in window appears, where the user enters the Intuit user ID (email) and password.
    /// 3.this app sends an authentication request to the Intuit OpenID service.
    /// 4.This page verifies the authentication response it receives from the Intuit OpenID service and stores
    /// user information inside the session object.
    /// </summary>
    public class OpenIdController : BaseController
    {
        /// <summary>
        /// OpenId Relying Party
        /// </summary>
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();

        /// <summary>
        /// Action Results for Index, uses DotNetOpenAuth for creating OpenId Request with Intuit
        /// and handling response recieved. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var openid_identifier = ConfigurationManager.AppSettings["openid_identifier"].ToString(); ;
            var response = openid.GetResponse();
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(openid_identifier, out id))
                {
                    try
                    {
                        IAuthenticationRequest request = openid.CreateRequest(openid_identifier);
                        FetchRequest fetch = new FetchRequest();
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Email));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.FullName));

                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Phone.Mobile));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Company.CompanyName));

                        request.AddExtension(fetch);
                        request.RedirectToProvider();
                    }
                    catch (ProtocolException ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                if (response.FriendlyIdentifierForDisplay == null)
                {
                    Response.Redirect("/OpenId");
                }

                // Stage 3: OpenID Provider sending assertion response, storing the response in Session object is only for demonstration purpose
                Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                FetchResponse fetch = response.GetExtension<FetchResponse>();
                if (fetch != null)
                {
                    QBUser qbusr = QBUser;
                  
                    qbusr.OpenIdResponse = true;
                    qbusr.QBEmail = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);
                    qbusr.Name = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);
                    qbusr.Mobile = fetch.GetAttributeValue(WellKnownAttributes.Contact.Phone.Mobile);
                    qbusr.CompanyName = fetch.GetAttributeValue(WellKnownAttributes.Company.CompanyName);
                    QBUser = qbusr;

                    //Session["OpenIdResponse"] = "True";
                    //Session["FriendlyEmail"] = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);// emailAddresses.Count > 0 ? emailAddresses[0] : null;
                    //Session["FriendlyName"] = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);//fullNames.Count > 0 ? fullNames[0] : null;

                    //Session["Mobile"] = fetch.GetAttributeValue(WellKnownAttributes.Contact.Phone.Mobile);// emailAddresses.Count > 0 ? emailAddresses[0] : null;
                    //Session["CompanyName"] = fetch.GetAttributeValue(WellKnownAttributes.Company.CompanyName);//fullNames.Count > 0 ? fullNames[0] : null;

                    ////get the Oauth Access token for the user from OauthAccessTokenStorage.xml
                    //OauthAccessTokenStorageHelper.GetOauthAccessTokenForUser(Session["FriendlyEmail"].ToString(), this);
                }
            }

            string query = Request.Url.Query;
            if (!string.IsNullOrWhiteSpace(query) && query.ToLower().Contains("disconnect=true"))
            {
                Session["accessToken"] = "dummyAccessToken";
                Session["accessTokenSecret"] = "dummyAccessTokenSecret";
                Session["Flag"] = true;
                return Redirect("/CleanupOnDisconnect/Index");
            }
            return OAuthConnect();
        }

        private ActionResult OAuthConnect()
        {
            QBUser qbusr = QBUser;
            if (qbusr != null)
            {
                string fEmail = qbusr.QBEmail, fName = qbusr.Name, company = qbusr.CompanyName;
                if (!string.IsNullOrEmpty(fEmail) && !string.IsNullOrEmpty(fName))
                {
                    //Verify User/AccesToken Exist for this user
                    IBSSignUP model = VerifyUserAndAccesToken();
                    if (ValidateUser(model))
                    {
                        qbusr.Releam = model.QBParamObj.Releam;
                        qbusr.DataSource = model.QBParamObj.DataSource;

                        qbusr.AccesKey = model.QBParamObj.AccesKey;
                        qbusr.AccesSecret = model.QBParamObj.AccesSecret;
                        QBUser = qbusr;
                        return RedirectToAction("SignUpOrLogin", "IBSAccount");
                    }
                }
            }
            return RedirectToAction("Index", "OauthGrant");
        }

        private bool ValidateUser(IBSSignUP model)
        {
            if (model == null)
                return false;
            else
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)
                    || string.IsNullOrEmpty(model.QBParamObj.QBEmail) || string.IsNullOrEmpty(model.QBParamObj.AccesKey)
                    || string.IsNullOrEmpty(model.QBParamObj.AccesSecret) || string.IsNullOrEmpty(model.QBParamObj.Releam))
                    return false;
            }
            return true;
        }

        private IBSSignUP VerifyUserAndAccesToken()
        {
            IBSQBService qbsrv = new IBSQBService();
            QBUser qbusr = QBUser;
            if (WebSecurity.HasUserId)
            {
                return qbsrv.GetOauthAccessTokenForUser(WebSecurity.CurrentUserId);
            }
            else
            {
                return qbsrv.GetOauthAccessTokenForUser(qbusr.QBEmail, qbusr.CompanyName);
            }

        }
    }
}
