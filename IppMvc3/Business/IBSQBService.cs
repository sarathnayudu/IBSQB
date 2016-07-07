using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Intuit.Ipp.Data;
using IntuitSampleMVC.Entity;
using IntuitSampleMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WebMatrix.WebData;

namespace IntuitSampleMVC.Business
{
    public class IBSQBService : BusinessBase
    {
        public CustomerModel GetQBCustomers()
        {
            List<CustomerUI> lstCustUI= DataService.FindAll(new  Customer(), 1, 100).Select(e => new CustomerUI
            {
                Id = e.Id,
                Name = e.FullyQualifiedName,
                ContactNumber = e.Mobile.FreeFormNumber,
                Email = e.PrimaryEmailAddr.Address,
                Notes = e.Notes

            }).ToList();

            CustomerModel custmodel = new CustomerModel();
            custmodel.LstCustomerUI = new List<CustomerUI>();
            custmodel.LstCustomerUI = lstCustUI;
            return custmodel;

        }
      

        public static string ReconnectRealm(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create("https://appcenter.intuit.com/api/v1/Connection/Reconnect") as HttpWebRequest;
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", GetDevDefinedOAuthHeader(httpWebRequest, consumerKey, consumerSecret, accessToken, accessTokenSecret));
            UTF8Encoding encoding = new UTF8Encoding();
            HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            using (Stream data = httpWebResponse.GetResponseStream())
            {
                //return XML response
                return new StreamReader(data).ReadToEnd();
            }
        }

        static string GetDevDefinedOAuthHeader(HttpWebRequest webRequest, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {

            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true

            };

            consumerContext.UseHeaderForOAuthParameters = true;

            //URIs not used - we already have Oauth tokens
            OAuthSession oSession = new OAuthSession(consumerContext, "https://www.example.com",
                                    "https://www.example.com",
                                    "https://www.example.com");


            oSession.AccessToken = new TokenBase
            {
                Token = accessToken,
                ConsumerKey = consumerKey,
                TokenSecret = accessTokenSecret
            };

            IConsumerRequest consumerRequest = oSession.Request();
            consumerRequest = ConsumerRequestExtensions.ForMethod(consumerRequest, webRequest.Method);
            consumerRequest = ConsumerRequestExtensions.ForUri(consumerRequest, webRequest.RequestUri);
            consumerRequest = consumerRequest.SignWithToken();
            return consumerRequest.Context.GenerateOAuthParametersForHeader();
        }


        internal void RemoveInvalidOauthAccessToken(int  userID)
        {
            ibshr121414Entities entity = new ibshr121414Entities();
            UserProfile uf = entity.UserProfiles.Where(e => e.UserId == userID).FirstOrDefault();
            if (uf != null)
            {
                uf.RelamID = string.Empty;
                uf.AccesKey = string.Empty;
                uf.AccesSecret = string.Empty;
                uf.DataSource = string.Empty;
                uf.QBEmail = string.Empty;
            }
            entity.SaveChanges();
        }

        internal IBSSignUP GetOauthAccessTokenForUser(string emailID,string companyName)
        {
            ibshr121414Entities entity = new ibshr121414Entities();

            UserProfile uf = entity.UserProfiles.Where(e => e.QBEmail == emailID).FirstOrDefault();
            IBSSignUP model = new IBSSignUP();

            if (uf != null)
            {
                model.CompanyName = uf.CompanyName;
                model.Country = uf.Country;
                model.Email = uf.Email;
                model.Name = uf.UserName;
                model.PhoneNumber = uf.PhoneNumber;
                model.QBParamObj = new QBParam();
                model.QBParamObj.AccesKey = uf.AccesKey;
                model.QBParamObj.AccesSecret = uf.AccesSecret;
                model.QBParamObj.Releam = uf.RelamID;
                model.QBParamObj.QBEmail = uf.QBEmail;
                model.QBParamObj.DataSource = uf.DataSource;
            }
            return model;
        }

        internal void StoreOauthAccessToken(IBSSignUP signup,string LocaluserEmail)
        {
            ibshr121414Entities entity = new ibshr121414Entities();
            UserProfile uf = entity.UserProfiles.Where(e => e.Email == LocaluserEmail).FirstOrDefault();
            if (uf != null)
            {
                uf.AccesKey = signup.QBParamObj.AccesKey;
                uf.AccesSecret = signup.QBParamObj.AccesSecret;
                uf.RelamID = signup.QBParamObj.Releam;
                uf.DataSource = signup.QBParamObj.DataSource;
                uf.UpdatedDate = DateTime.Now;
                uf.QBEmail = signup.QBParamObj.QBEmail;
                uf.CompanyName = signup.CompanyName;
            }

            entity.SaveChanges();
        }

        internal void StoreOauthAccessToken(IBSSignUP signup, int LocaluserId)
        {
            ibshr121414Entities entity = new ibshr121414Entities();
            UserProfile uf = entity.UserProfiles.Where(e => e.UserId == LocaluserId).FirstOrDefault();
            if (uf != null)
            {
                uf.AccesKey = signup.QBParamObj.AccesKey;
                uf.AccesSecret = signup.QBParamObj.AccesSecret;
                uf.RelamID = signup.QBParamObj.Releam;
                uf.DataSource = signup.QBParamObj.DataSource;
                uf.UpdatedDate = DateTime.Now;
                uf.QBEmail = signup.QBParamObj.QBEmail;
                uf.CompanyName = signup.CompanyName;
            }

            entity.SaveChanges();
        }

        public void UpdateUserDetails(IBSSignUP model)
        {
            ibshr121414Entities entity = new ibshr121414Entities();
            UserProfile uf = entity.UserProfiles.Where(e => e.QBEmail == model.Email).FirstOrDefault();
            if (uf != null)
            {
                uf.UserName = model.Name;
                uf.CompanyName = model.CompanyName;
                uf.Country = model.Country;               
                uf.CreatedDate = DateTime.Now;
                uf.UpdatedDate = DateTime.Now;
                // uf.Email = model.Email;
                uf.PhoneNumber = model.PhoneNumber;
                // entity.UserProfiles.Add(uf);
                uf.webpages_Roles.Add(entity.webpages_Roles.FirstOrDefault());

                uf.AccesKey = model.QBParamObj.AccesKey;
                uf.AccesSecret = model.QBParamObj.AccesSecret;
                uf.RelamID = model.QBParamObj.Releam;
                uf.DataSource = model.QBParamObj.DataSource;
                uf.QBEmail = model.QBParamObj.QBEmail;
                uf.UpdatedDate = DateTime.Now;

                entity.SaveChanges();
            }
        }

        internal IBSSignUP GetOauthAccessTokenForUser(int p)
        {
            ibshr121414Entities entity = new ibshr121414Entities();

            UserProfile uf = entity.UserProfiles.Where(e => e.UserId == p).FirstOrDefault();
            IBSSignUP model = new IBSSignUP();

            if (uf != null)
            {
                model.CompanyName = uf.CompanyName;
                model.Country = uf.Country;
                model.Email = uf.Email;
                model.Name = uf.UserName;
                model.PhoneNumber = uf.PhoneNumber;
                model.QBParamObj = new QBParam();
                model.QBParamObj.AccesKey = uf.AccesKey;
                model.QBParamObj.AccesSecret = uf.AccesSecret;
                model.QBParamObj.Releam = uf.RelamID;
                model.QBParamObj.QBEmail = uf.QBEmail;
                model.QBParamObj.DataSource = uf.DataSource;               
            }
            return model;
        }

        internal IBSSignUP GetOauthAccessTokenForUser(string  uemail)
        {
            ibshr121414Entities entity = new ibshr121414Entities();

            UserProfile uf = entity.UserProfiles.Where(e => e.Email == uemail).FirstOrDefault();
            IBSSignUP model = new IBSSignUP();

            if (uf != null)
            {
                model.CompanyName = string.IsNullOrEmpty(uf.CompanyName)?string.Empty: uf.CompanyName.Trim();
                model.Country = string.IsNullOrEmpty(uf.Country) ? string.Empty : uf.Country.Trim();
                model.Email = string.IsNullOrEmpty(uf.Email) ? string.Empty : uf.Email.Trim();
                model.Name = string.IsNullOrEmpty(uf.UserName) ? string.Empty : uf.UserName.Trim();
                model.PhoneNumber = uf.PhoneNumber;
                model.QBParamObj = new QBParam();
                model.QBParamObj.AccesKey = uf.AccesKey;
                model.QBParamObj.AccesSecret = uf.AccesSecret;
                model.QBParamObj.Releam = uf.RelamID;
                model.QBParamObj.QBEmail = string.IsNullOrEmpty(uf.QBEmail) ? string.Empty : uf.QBEmail.Trim();
                model.QBParamObj.DataSource = uf.DataSource;
            }
            return model;
        }


        internal bool QBCompanyUserExist(IBSSignUP signup)
        {
            ibshr121414Entities entity = new ibshr121414Entities();

            UserProfile uf = entity.UserProfiles.Where(e => e.QBEmail == signup.QBParamObj.QBEmail).FirstOrDefault();

            return !string.IsNullOrEmpty(uf.Email);
        }

        internal void Trackrequest(string s)
        {
            ibshr121414Entities entity = new ibshr121414Entities();
            entity.Requests.Add(new Request
            {
                Request1=s,
                
            });

            entity.SaveChanges();
        }
    }
}