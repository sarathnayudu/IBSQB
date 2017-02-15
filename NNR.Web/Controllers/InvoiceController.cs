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

        public ActionResult Create(InvoicePreferences model)
        {
            if (ModelState.IsValid)
            {
                CustomerInvoice custInv = new CustomerInvoice(model.CustQBId,Convert.ToDateTime(model.InvoiceDate), Convert.ToDateTime(model.Duedate), model.Crew,
                   model.SelectedDiscountTypeId, model.DiscountValue,model.Memo, model.InvoiceMessage,
                  model.SelectedTermId, model.SelectedProductId, model.SelectedTaxId);
                custInv.NewInvoice();
                return RedirectToAction("Index", "Home");
            }
            QuickBookBlogic qblog = new QuickBookBlogic();
            InvoicePreferences modelNew= qblog.GetCustomerPreferences(model.CustQBId);
            model.ListProduct = modelNew.ListProduct;
            model.ListTerms = modelNew.ListTerms;
            model.Tax= modelNew.Tax;
            model.DiscountType = modelNew.DiscountType;
            return View("Index",model);
        }

        public ActionResult Productpartial(int prodid)
        {
            QuickBookBlogic qblog = new QuickBookBlogic();
            List<ServiceDetails> Listsevdetail = qblog.GetServiceDetails(prodid);

            return PartialView(Listsevdetail);
        }
    }
}