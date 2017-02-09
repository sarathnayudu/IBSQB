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
        [Required]
        [MinLength(1, ErrorMessage = "* Select A term ID")]
        public string CustQBId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
       
        public string BillingAdress { get; set; }
        [Required]
        [Range(0, int.MaxValue,ErrorMessage = "* Select A term ID")]
        public int SelectedTermId { get; set; }
      
        public List<ComboBase> ListTerms { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "* Select A term ID")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Duedate { get; set; }
        [Required]
        public string Crew { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "* Select A term ID")]
        public int SelectedProductId { get; set; }
        public List<ComboBase> ListProduct { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "* Select A term ID")]
        public int SelectedTaxId { get; set; }
        public List<ComboBase> Tax { get; set; }

        public int SelectedDiscountTypeId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "* Select A term ID")]
        public List<ComboBase> DiscountType { get; set; }

        public double DiscountValue { get; set; }

        public decimal AmountReceived { get; set; }

        public double  Balance { get; set; }

        public string InvoiceMessage { get; set; }
        public string Memo { get; set; }
        public string Attachments { get; set; }
            
    }
}