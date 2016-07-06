using Intuit.Ipp.Data;
using IntuitSampleMVC.Business;
using IntuitSampleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IntuitSampleMVC.Controllers
{
    public class IBSInvoiceController : BaseController
    {
        //
        // GET: /IBSInvoice/

        public ActionResult Index()
        {
            IBSQBCustomer obj = new IBSQBCustomer();
           
            return View("QBCustomers", obj.GetQBCustomers());
        }

        [HttpPost]
        public ActionResult NewInvoice(string[] Selected)
        {
             //Generate Invoice
            IBSQBInvoice inv = new IBSQBInvoice();
            foreach (string custId in Selected)
            {

                inv.NewInvoice(custId);
            }

            return View();
        }

    }
}
