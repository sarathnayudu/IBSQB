using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NNR.Web.Models
{
    public class InvoicePreferences
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string BillingAdress { get; set; }
        public string Terms { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime Duedate { get; set; }
       // public Product ProductModel { get; set; }
        public string InvoiceMessage { get; set; }
        public string Memo { get; set; }
        public string Attachments { get; set; }
            
    }
}