using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class NewInvoice:ModelBase
    {
        public string CustName { get; set; }
        public string Email { get; set; }
        public string BillingAdress { get; set; }
        public List<string> Terms { get; set; }
        public string InvDate { get; set; }
        public string  DueDate { get; set; }
        public List<ProductOrService> LstProductorSrv { get; set; }
        public List<string> TaxCodes { get; set; }

    }
}