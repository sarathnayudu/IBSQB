using Intuit.Ipp.Data;
using IntuitSampleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Business
{
    public class IBSQBService:BusinessBase
    {
        public List<CustomerUI> GetQBCustomers()
        {
            return DataService.FindAll(new Customer(), 1, 100).Select(e => new CustomerUI
            {
                CustEmpVendorObj = e
            }).ToList();

        }

        public List<CustomerUI> GetQBVendors()
        {
            return DataService.FindAll(new Vendor(), 1, 100).Select(e => new CustomerUI
            {
                CustEmpVendorObj = e
            }).ToList();

        }
        public List<CustomerUI> GetQBEmployes()
        {
            return DataService.FindAll(new Employee(), 1, 100).Select(e => new CustomerUI
            {
                CustEmpVendorObj = e
            }).ToList();

        }
    }
}