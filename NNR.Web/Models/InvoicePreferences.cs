using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNR.Web.Models
{
    public class InvoicePreferences
    {
        public int CustomerId { get; set; }
        public string CustQBId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string BillingAdress { get; set; }

        public string SelectedTermId { get; set; }
        public List<ComboBase> ListTerms { get; set; }
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime Duedate { get; set; }

        public string Crew { get; set; }

        public string SelectedProductId { get; set; }
        public List<ComboBase> ListProduct { get; set; }

        public string SelectedTaxId { get; set; }
        public List<ComboBase> Tax { get; set; }

        public string SelectedDiscountTypeId { get; set; }
        public List<ComboBase> DiscountType { get; set; }

        public double DiscountValue { get; set; }

        public decimal AmountReceived { get; set; }

        public double  Balance { get; set; }

        public string InvoiceMessage { get; set; }
        public string Memo { get; set; }
        public string Attachments { get; set; }
            
    }
}