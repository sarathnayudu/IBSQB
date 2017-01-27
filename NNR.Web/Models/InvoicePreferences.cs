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
        public List<ComboBase> ListTerms { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime Duedate { get; set; }

        public string Crew { get; set; }

        public List<ComboBase> ListProduct { get; set; }

        public List<ComboBase> Tax { get; set; }

        public List<ComboBase> DiscountType { get; set; }

        public double DiscountValue { get; set; }

        public double AmountReceived { get; set; }

        public double  Balance { get; set; }

        public string InvoiceMessage { get; set; }
        public string Memo { get; set; }
        public string Attachments { get; set; }
            
    }
}