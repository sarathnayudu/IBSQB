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
            CustomerInvoice custInv = new CustomerInvoice(fc["CustQBId"], fc["InvoiceDate"], fc["Duedate"], fc["Crew"],
                fc["SelectedDiscountTypeId"], fc["DiscountValue"], fc["Memo"], fc["InvoiceMessage"],
               fc["SelectedTermId"], fc["SelectedProductId"], fc["SelectedTaxId"]);

            return View();
        }

        public ActionResult Productpartial(int prodid)
        {
            QuickBookBlogic qblog = new QuickBookBlogic();
            List<ServiceDetails> Listsevdetail = qblog.GetServiceDetails(prodid);

            return PartialView(Listsevdetail);
        }
    }
}