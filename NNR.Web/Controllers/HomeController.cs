using NNR.Web.BLogic;
using NNR.Web.Models;
using NNR.Web.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNR.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty((string)Session["accessToken"])
      && !string.IsNullOrEmpty((string)Session["accessTokenSecret"]))
            {
                return RedirectToAction("Index", "QuickBooksCustomers");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}