using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class CustomerModel:ModelBase
    {
        public string SelTaxCode { get; set; }
        public string SelItem { get; set; }
        public string SelTerms { get; set; }

        public List<CustomerUI> LstCustomerUI { get; set; }
    }
}