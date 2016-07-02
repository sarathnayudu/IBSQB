using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class ProductOrService
    {
        public List<string> ProductOrSrvName { get; set; }
        public string Desc { get; set; }
        public int  Quantity { get; set; }
        public Double  Rate { get; set; }
        public double  Amount { get; set; }
        public bool  Taxable { get; set; }

    }
}