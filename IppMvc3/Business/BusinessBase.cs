using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using IntuitSampleMVC.Entity;
using IntuitSampleMVC.Models;

namespace IntuitSampleMVC.Business
{
    public class BusinessBase
    {
        /// <summary>
        /// RealmId, AccessToken, AccessTokenSecret, ConsumerKey, ConsumerSecret, DataSourceType
        /// </summary>
        public String realmId, accessToken, accessTokenSecret, consumerKey, consumerSecret, dataSourcetype;

        public BusinessBase()
        {
            QBUser qbusr = (QBUser)HttpContext.Current.Session["QBUser"];
            if (qbusr != null)
            {
                realmId = qbusr.Releam;
                accessToken = qbusr.AccesKey;
                accessTokenSecret = qbusr.AccesSecret;
                consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
                consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
                dataSourcetype = qbusr.DataSource;
            }

            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(accessTokenSecret) && !string.IsNullOrEmpty(realmId))
            {
                OAuthRequestValidator = new OAuthRequestValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);
                ServiceContext = new ServiceContext(realmId, IntuitServicesType.QBO, OAuthRequestValidator);
                //ServiceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";

                DataService = new DataService(ServiceContext);
            }
        }

        private OAuthRequestValidator _oAuthRequestValidator;

        private OAuthRequestValidator OAuthRequestValidator
        {
            get { return _oAuthRequestValidator; }
            set { _oAuthRequestValidator = value; }
        }

        private ServiceContext _serviceContext;

        private ServiceContext ServiceContext
        {
            get { return _serviceContext; }
            set { _serviceContext = value; }
        }
        private DataService _dataService;

        public DataService DataService
        {
            get { return _dataService; }
            set { _dataService = value; }
        }

        public ibshr121414Entities _context 
        {
            get
            {
                return new ibshr121414Entities();
            }
           

        }
      
    }
}