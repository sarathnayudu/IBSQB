using IntuitSampleMVC.Business;
using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


namespace IntuitSampleMVC.Controllers
{
    public class LogoutController : BaseController
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            IBSQBService qbs = new IBSQBService();
            qbs.RemoveInvalidOauthAccessToken(WebSecurity.CurrentUserId);
          //  OauthAccessTokenStorageHelper.RemoveInvalidOauthAccessToken(Session["FriendlyEmail"].ToString(), this);
            Session.RemoveAll();
            
            //Redirect user to the Home page
            return RedirectToAction("IBSHome", "IBSAccount");
        }
    }
}
