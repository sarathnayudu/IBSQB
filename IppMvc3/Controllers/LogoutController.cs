using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IntuitSampleMVC.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            OauthAccessTokenStorageHelper.RemoveInvalidOauthAccessToken(Session["FriendlyEmail"].ToString(), this);
            Session.RemoveAll();
            
            //Redirect user to the Home page
            return Redirect("/IBSAccount/LogOff");

        }

    }
}
