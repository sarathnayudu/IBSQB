using IntuitSampleMVC.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    public class IBSQBController : Controller
    {
        //
        // GET: /IBSQB/

        public ActionResult Index()
        {
            IBSQBService obj = new IBSQBService();

            return View("../IBSInvoice/QBCustomers", obj.GetQBCustomers());
        }

        public ActionResult Vendors()
        {
            IBSQBService obj = new IBSQBService();

            return View("../IBSInvoice/QBCustomers", obj.GetQBVendors());
        }

        public ActionResult Employes()
        {
            IBSQBService obj = new IBSQBService();

            return View("../IBSInvoice/QBCustomers", obj.GetQBEmployes());
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
