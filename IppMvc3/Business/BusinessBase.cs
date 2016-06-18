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
            realmId = HttpContext.Current.Session["realm"]!=null?HttpContext.Current.Session["realm"].ToString():string.Empty;
            accessToken =HttpContext.Current.Session["accessToken"]!=null? HttpContext.Current.Session["accessToken"].ToString():string.Empty;
            accessTokenSecret = HttpContext.Current.Session["accessTokenSecret"]!=null?HttpContext.Current.Session["accessTokenSecret"].ToString():string.Empty;
            consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
            consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
            dataSourcetype = HttpContext.Current.Session["dataSource"] != null ? HttpContext.Current.Session["dataSource"].ToString().ToLower() : string.Empty;

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