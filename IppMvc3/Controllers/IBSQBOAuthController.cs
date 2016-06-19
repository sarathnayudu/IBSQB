using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    public class IBSQBOAuthController : Controller
    {
        //
        // GET: /IBSQBOAuth/
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        public ActionResult Index()
        {
            List<string> lstkeys = new List<string>();
            var openid_identifier = ConfigurationManager.AppSettings["openid_identifier"].ToString(); ;
            var response = openid.GetResponse();
            if (response == null)
            {
                lstkeys.Add("responce null");
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
                lstkeys.Add("responce full");
                if (response.FriendlyIdentifierForDisplay == null)
                {
                    Response.Redirect("/OpenId");
                }

                // Stage 3: OpenID Provider sending assertion response, storing the response in Session object is only for demonstration purpose
                Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                FetchResponse fetch = response.GetExtension<FetchResponse>();
                if (fetch != null)
                {
                    Session["OpenIdResponse"] = "True";
                    Session["FriendlyEmail"] = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);// emailAddresses.Count > 0 ? emailAddresses[0] : null;
                    Session["FriendlyName"] = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);//fullNames.Count > 0 ? fullNames[0] : null;

                    //get the Oauth Access token for the user from OauthAccessTokenStorage.xml
                    OauthAccessTokenStorageHelper.GetOauthAccessTokenForUser(Session["FriendlyEmail"].ToString(), this);
                }


            }

            foreach (string key in Request.QueryString.Keys)
            {

                string s = key + "/////" + Request.QueryString[key];
                lstkeys.Add(s);

            }
            if (Session["FriendlyEmail"] != null)
                lstkeys.Add(Session["FriendlyEmail"].ToString());
            ViewBag.lst = lstkeys;

           return GrantMethod();





           // return View();
        }
        private String consumerSecret, consumerKey, oauthLink, RequestToken, TokenSecret, oauth_callback_url;
        private RedirectResult GrantMethod()
        {
             
            oauth_callback_url = Request.Url.GetLeftPart(UriPartial.Authority) + ConfigurationManager.AppSettings["oauth_callback_url"];
            consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            oauthLink = Constants.OauthEndPoints.IdFedOAuthBaseUrl;
            IToken token = (IToken)Session["requestToken"];
            IOAuthSession session = CreateSession();
            IToken requestToken = session.GetRequestToken();
            Session["requestToken"] = requestToken;
            RequestToken = requestToken.Token;
            TokenSecret = requestToken.TokenSecret;

            oauthLink = Constants.OauthEndPoints.AuthorizeUrl + "?oauth_token=" + RequestToken + "&oauth_callback=" + UriUtility.UrlEncode(oauth_callback_url);
            return Redirect(oauthLink);
        
        }

        /// <summary>
        /// Gets the Access Token
        /// </summary>
        /// <returns>Returns OAuth Session</returns>
        protected IOAuthSession CreateSession()
        {
            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext,
                                            Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlRequestToken,
                                            oauthLink,
                                            Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlAccessToken);
        }

    }
}

