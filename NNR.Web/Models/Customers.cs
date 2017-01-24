using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NNR.Web.Models
{
    public class Customers
    {
        public decimal Balance { get; internal set; }
        public string CompanyName { get; internal set; }
        public decimal CreditLimit { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FullyQualifiedName { get; internal set; }
        public string Id { get; internal set; }
        public bool IsSelected { get; internal set; }
        public decimal TotalExpense { get; internal set; }
        public string UserId { get; internal set; }
    }
}