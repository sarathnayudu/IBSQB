using NNR.Web.BLogic;
using NNR.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNR.Web.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index(string custqbId)
        {
            QuickBookBlogic qblog = new QuickBookBlogic();
            InvoicePreferences invpref =  qblog.GetCustomerPreferences(custqbId);        

            return View(invpref);
        }

        public ActionResult Create(FormCollection fc)
        {

            return View();
        }
    }
}