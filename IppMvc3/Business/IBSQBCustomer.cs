using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using IntuitSampleMVC.Models;

namespace IntuitSampleMVC.Business
{
    public class IBSQBCustomer:BusinessBase
    {
        public List<CustomerUI> GetQBCustomers()
        {
         return  DataService.FindAll(new Customer(), 1, 100).Select(e=> new CustomerUI
              {
                  Customer=e
              }).ToList();

        }
    }
}