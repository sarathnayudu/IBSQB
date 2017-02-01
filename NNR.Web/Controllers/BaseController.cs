using NNR.Web.BLogic;
using NNR.Web.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNR.Web.Controllers
{
    public class BaseController :Controller
    {
      
        public void FillQbUserDetails(string userEmail)
        {
            QuickBookBlogic qblog = new QuickBookBlogic();
            QbUser qbusr = qblog.GetQbUserbyUserEmail(userEmail);
            if (qbusr != null)
                OauthAccessTokenStorageHelper.GetOauthAccessTokenForUser(qbusr.Email, this);
        }
    }
}