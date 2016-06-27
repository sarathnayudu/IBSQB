using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using IntuitSampleMVC.Business;
using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    public class IBSQBOAuthController : Controller
    {
        //
        // GET: /IBSQBOAuth/
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        public ActionResult Index(int flag=0)
        {
           //string s= Request.Url.AbsoluteUri;
           //IBSQBService srv = new IBSQBService();
           //srv.Trackrequest(s);
            if (flag != 0)
            {              
                return RedirectToAction("Index", "OpenId");
                //return RedirectToAction("SignUpOrLogin", "IBSAccount");
            }
            else
            {
                return RedirectToAction("Index", "Home");               
            }              
        }      
    }
}

