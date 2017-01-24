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
            return View();
        }
    }
}