using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class CustomerUI:ModelBase
    {
        public bool IsChecked { get; set; }

        public Customer Customer { get; set; }
    }
}