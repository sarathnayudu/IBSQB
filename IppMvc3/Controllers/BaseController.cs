using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntuitSampleMVC.Models;
using IntuitSampleMVC.Business;
using IntuitSampleMVC.Services;
using System.Web.Routing;
using System.Configuration;
using IntuitSampleMVC.utils;

namespace IntuitSampleMVC.Controllers
{
    public class BaseController : Controller
    {
        QBUser qbusr;
        public IWebSecurityService WebSecurityService { get; set; }
        public IMessengerService MessengerService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (WebSecurityService == null) { WebSecurityService = new WebSecurityService(); }
            if (MessengerService == null) { MessengerService = new MessengerService(); }

            base.Initialize(requestContext);
        }
        public QBUser QBUser
        {
            get
            {
                qbusr = (QBUser)Session["QBUser"];
                if (qbusr == null)
                    qbusr = new QBUser();
                return qbusr;
            }
            set
            {
                Session["QBUser"] = value;
            }
        }
        public void FillQBDataIfAny(string userEmail)
        {
            IBSQBService qbsrv = new IBSQBService();
            IBSSignUP model = null;
            UpdateUserQBData(userEmail);
            if (!WebSecurityService.HasUserId)
                model = qbsrv.GetOauthAccessTokenForUser(userEmail);
            else
                model = qbsrv.GetOauthAccessTokenForUser(WebSecurityService.CurrentUserId);
            if (ValidateUser(model))
            {
                QBUser qbusr = QBUser;

                string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];
                qbusr.QBEmail = model.QBParamObj.QBEmail;
                qbusr.CompanyName = model.QBParamObj.QBCompanyName;
                qbusr.Releam = model.QBParamObj.Releam;
                qbusr.DataSource = model.QBParamObj.DataSource;

                qbusr.AccesKey = CryptographyHelper.DecryptData(model.QBParamObj.AccesKey, secuirtyKey);
                qbusr.AccesSecret = CryptographyHelper.DecryptData(model.QBParamObj.AccesSecret, secuirtyKey);
                QBUser = qbusr;
            }
        }
        public void UpdateUserQBData(string userEmail)
        {
            QBUser qbusr = QBUser;
            if (!string.IsNullOrEmpty(qbusr.QBEmail))
            {
                IBSQBService srv = new IBSQBService();
                IBSSignUP signup = new IBSSignUP();

                signup.QBParamObj = new QBParam();
                string secuirtyKey = ConfigurationManager.AppSettings["securityKey"];

                signup.QBParamObj.AccesKey = CryptographyHelper.EncryptData(qbusr.AccesKey, secuirtyKey);
                signup.QBParamObj.AccesSecret = CryptographyHelper.EncryptData(qbusr.AccesSecret, secuirtyKey);
                signup.QBParamObj.Releam = qbusr.Releam;
                signup.QBParamObj.DataSource = qbusr.DataSource;
                signup.QBParamObj.QBEmail = qbusr.QBEmail;
                signup.QBParamObj.QBCompanyName = qbusr.CompanyName;

                if (WebSecurityService.CurrentUserId == -1)
                    srv.StoreOauthAccessToken(signup, userEmail);
                else
                    srv.StoreOauthAccessToken(signup, WebSecurityService.CurrentUserId);
            }
        }
        public bool ValidateUser(IBSSignUP model)
        {
            bool isValid = true;
            if (model == null)
                isValid = false;
            else
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.QBParamObj.AccesKey)
                    || string.IsNullOrEmpty(model.QBParamObj.AccesSecret) || string.IsNullOrEmpty(model.QBParamObj.Releam) || string.IsNullOrEmpty(model.QBParamObj.DataSource))
                    isValid = false;
            }
            return isValid;
        }
    }
}
