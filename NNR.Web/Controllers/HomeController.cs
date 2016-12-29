using NNR.Web.Models;
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
            if (Session["show"] != null)
            {
                bool value = System.Convert.ToBoolean(Session["show"]);
                if (value)
                {
                    Session.Remove("show");
                    //show a message to the user that token is invalid
                    string message = "<SCRIPT LANGUAGE='JavaScript'>alert('Your authorization to this application to access your quickbook data is no longer Valid.Please provide authorization again.')</SCRIPT>";
                    // show user the connect to quickbook page again
                    Response.Write(message);
                }
            }
            return View();
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