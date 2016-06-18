using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    public class IBSQBOAuthController : Controller
    {
        //
        // GET: /IBSQBOAuth/

        public ActionResult Index()
        {
            OauthAccessTokenStorageHelper.StoreOauthAccessToken(this);
           string s=Session["FriendlyEmail"].ToString();
            return View();
        }

    }
}
