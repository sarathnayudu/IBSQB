using NNR.Web.BLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNR.Web.Controllers
{
    public class SyncController : Controller
    {
        // GET: Sync
        public ActionResult SyncNow()
        {
            QuickBookBlogic qblogic = new QuickBookBlogic();
            qblogic.SyncNow();

            return RedirectToAction("Index", "Home");
        }
    }
}