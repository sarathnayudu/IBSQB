using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NNR.Web.Models
{
    public class InvoiceItems
    {
        public List<ComboBase> ListProductOrServices { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double  Amount { get; set; }
        public double TaxPercent { get; set; }
        public double AfterTax { get; set; }

        public double Discount { get; set; }

        public double AmountReceived { get; set; }
    }
}