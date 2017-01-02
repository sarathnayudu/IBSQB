using System;
using System.Xml;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using System.Web.Mvc;
using NNR.Web.BLogic;

namespace NNR.Web.utils
{
    /// <summary>
    /// This class stores and fetches Oauth Access token for an user from XML file. In real world it could be database or any other suitable storage.
    /// </summary>
    public static class OauthAccessTokenStorageHelper
    {
        /// <summary>
        /// Remove oauth access toekn from storage 
        /// </summary>
        /// <param name="emailID"></param>
        internal static void RemoveInvalidOauthAccessToken(string emailID, Controller page)
        {
            string path = page.Server.MapPath("/") + @"OauthAccessTokenStorage.xml";
            string searchUserXpath = "//record[@usermailid='" + emailID + "']";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode record = doc.SelectSingleNode(searchUserXpath);
            if (record != null)
            {
                doc.DocumentElement.RemoveChild(record);
                doc.Save(path);
            }

            //Rermove it from session
            page.Session.Remove("realm");
            page.Session.Remove("dataSource");
            page.Session.Remove("accessToken");
            page.Session.Remove("accessTokenSecret");
            page.Session.Remove("Flag");
        }

        /// <summary>
        /// get the oauth access token for the user from OauthAccessTokenStorage.xml
        /// </summary>
        /// <param name="emailID"></param>
        internal static QbUser GetOauthAccessTokenForUser(string emailID, Controller page)
        {
            //string path = page.Server.MapPath("/") + @"OauthAccessTokenStorage.xml";
            //string searchUserXpath = "//record[@usermailid='" + emailID + "']";
            //XmlDocument doc = new XmlDocument();
            //doc.Load(path);
            //XmlNode record = doc.SelectSingleNode(searchUserXpath);
            //if (record != null)
            //{
            //    page.Session["realm"] = record.Attributes["realmid"].Value;
            //    page.Session["dataSource"] = record.Attributes["dataSource"].Value;
            //    string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];
            //    page.Session["accessToken"] = CryptographyHelper.DecryptData(record.Attributes["encryptedaccesskey"].Value, secuirtyKey);
            //    page.Session["accessTokenSecret"] = CryptographyHelper.DecryptData(record.Attributes["encryptedaccesskeysecret"].Value, secuirtyKey);

            //    // Add flag to session which tells that accessToken is in session
            //    page.Session["Flag"] = true;
            //}

            QuickBookBlogic qbLog = new QuickBookBlogic();
             QbUser qbusr=  qbLog.GetQbuser(emailID);
            if (qbusr != null)
            {
                page.Session["realm"] =!string.IsNullOrEmpty(qbusr.RelaimID)? qbusr.RelaimID.Trim(): qbusr.RelaimID;
                page.Session["dataSource"] = !string.IsNullOrEmpty(qbusr.DataSource) ? qbusr.DataSource.Trim():qbusr.DataSource;
                string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];
                page.Session["accessToken"] = CryptographyHelper.DecryptData(qbusr.Token, secuirtyKey);
                page.Session["accessTokenSecret"] = CryptographyHelper.DecryptData(qbusr.Secret, secuirtyKey);

                // Add flag to session which tells that accessToken is in session
                page.Session["Flag"] = true;
            }
            return qbusr;
        }

        /// <summary>
        /// persist the Oauth access token in OauthAccessTokenStorage.xml file
        /// </summary>
        internal static void StoreOauthAccessToken(Controller page)
        {
            //string path = page.Server.MapPath("/") + @"OauthAccessTokenStorage.xml";
            //XmlDocument doc = new XmlDocument();
            //doc.Load(path);
            //XmlNode node = doc.CreateElement("record");
            //XmlAttribute userMailIdAttribute = doc.CreateAttribute("usermailid");
            //userMailIdAttribute.Value = page.Session["FriendlyEmail"].ToString();
            //node.Attributes.Append(userMailIdAttribute);

            //XmlAttribute accessKeyAttribute = doc.CreateAttribute("encryptedaccesskey");
            string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];
            //accessKeyAttribute.Value = CryptographyHelper.EncryptData(page.Session["accessToken"].ToString(), secuirtyKey);
            //node.Attributes.Append(accessKeyAttribute);

            //XmlAttribute encryptedaccesskeysecretAttribute = doc.CreateAttribute("encryptedaccesskeysecret");
            //encryptedaccesskeysecretAttribute.Value = CryptographyHelper.EncryptData(page.Session["accessTokenSecret"].ToString(), secuirtyKey);
            //node.Attributes.Append(encryptedaccesskeysecretAttribute);

            //XmlAttribute realmIdAttribute = doc.CreateAttribute("realmid");
            //realmIdAttribute.Value = page.Session["realm"].ToString();
            //node.Attributes.Append(realmIdAttribute);

            //XmlAttribute dataSourceAttribute = doc.CreateAttribute("dataSource");
            //dataSourceAttribute.Value = page.Session["dataSource"].ToString();
            //node.Attributes.Append(dataSourceAttribute);

            //doc.DocumentElement.AppendChild(node);
            //doc.Save(path);

            QuickBookBlogic qblogic = new QuickBookBlogic();
           QbUser qbusr= qblogic.GetQbuser(page.Session["FriendlyEmail"].ToString());
            if (qbusr == null)
            {
                qbusr = new QbUser();
                qbusr.DataSource = page.Session["dataSource"].ToString();
                qbusr.Email = page.Session["FriendlyEmail"].ToString();
                qbusr.name = page.Session["FriendlyEmail"].ToString();
                qbusr.RelaimID = page.Session["realm"].ToString();
                qbusr.Secret = CryptographyHelper.EncryptData(page.Session["accessTokenSecret"].ToString(), secuirtyKey);
                qbusr.Token = CryptographyHelper.EncryptData(page.Session["accessToken"].ToString(), secuirtyKey);
                QBEntities entity = new QBEntities();
                entity.QbUsers.Add(qbusr);
                entity.SaveChanges();
            }
        }


    }
}