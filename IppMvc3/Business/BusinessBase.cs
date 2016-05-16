using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;

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
            realmId = HttpContext.Current.Session["realm"].ToString();
            accessToken = HttpContext.Current.Session["accessToken"].ToString();
            accessTokenSecret = HttpContext.Current.Session["accessTokenSecret"].ToString();
            consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
            consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
            dataSourcetype = HttpContext.Current.Session["dataSource"].ToString().ToLower();

            OAuthRequestValidator = new OAuthRequestValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);
            ServiceContext = new ServiceContext(realmId, IntuitServicesType.QBO, OAuthRequestValidator);
            ServiceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";

            DataService = new DataService(ServiceContext);
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
        
        
    }
}