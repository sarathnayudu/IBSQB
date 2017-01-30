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
        public ActionResult Index(int Id)
        {
            InvoicePreferences invpref = new InvoicePreferences();
            invpref.ListTerms = new List<ComboBase>();
            invpref.ListProduct = new List<ComboBase>();
            invpref.DiscountType = new List<ComboBase>();

            return View(invpref);
        }

        public ActionResult Create(FormCollection fc)
        {
            return View();
        }
    }
}