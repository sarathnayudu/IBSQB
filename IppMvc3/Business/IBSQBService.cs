using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Intuit.Ipp.Data;
using IntuitSampleMVC.Entity;
using IntuitSampleMVC.Models;
using System;
using System.Collections.Generic;
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
        public List<CustomerUI> GetQBCustomers()
        {
            return DataService.FindAll(new Customer(), 1, 100).Select(e => new CustomerUI
            {
                CustEmpVendorObj = e
            }).ToList();

        }

        public List<CustomerUI> GetQBVendors()
        {
            return DataService.FindAll(new Vendor(), 1, 100).Select(e => new CustomerUI
            {
                CustEmpVendorObj = e
            }).ToList();
        }
        public List<CustomerUI> GetQBEmployes()
        {
            return DataService.FindAll(new Employee(), 1, 100).Select(e => new CustomerUI
            {
                CustEmpVendorObj = e
            }).ToList();

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


        internal void RemoveInvalidOauthAccessToken(string emailID)
        {
            UserProfile uf = _context.UserProfiles.Where(e => e.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            if (uf != null)
            {
                uf.RelamID = string.Empty;
                uf.AccesKey = string.Empty;
                uf.AccesSecret = string.Empty;
                uf.DataSource = string.Empty;
            }
            _context.SaveChanges();
        }

        internal List<string> GetOauthAccessTokenForUser(string emailID)
        {
            UserProfile uf = _context.UserProfiles.Where(e => e.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            
            List<string> lstkeys = new List<string>();
            if (uf != null)
            {
                lstkeys.Add(uf.AccesKey);
                lstkeys.Add(uf.AccesSecret);
                lstkeys.Add(uf.RelamID);
                lstkeys.Add(uf.DataSource);
            }
            return lstkeys;
        }

        internal void StoreOauthAccessToken(List<string> lstkeys)
        {
            UserProfile uf = _context.UserProfiles.Where(e => e.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            if (uf != null)
            {

                uf.AccesKey = lstkeys[0];
                uf.AccesSecret = lstkeys[1];
                uf.RelamID = lstkeys[2];
                uf.DataSource = lstkeys[3];
                uf.UpdatedDate = DateTime.Now;
            } 
               
                _context.SaveChanges();
        }
    }
}