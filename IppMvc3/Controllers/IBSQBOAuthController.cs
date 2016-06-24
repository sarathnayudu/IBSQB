using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
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
            if (flag == 0)
            {
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("SignUpOrLogin", "IBSAccount");
            }
            else
            {
                return RedirectToAction("Index", "OpenId");
            }              
        }      
    }
}

