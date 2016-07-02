using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class CustomerModel:ModelBase
    {      

        public List<CustomerUI> LstCustomerUI { get; set; }
    }
}