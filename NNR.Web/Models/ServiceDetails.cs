using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NNR.Web.Models
{
    public class ServiceDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float QTY { get; set; }
        public float Rate { get; set; }
        public decimal Amount { get; set; }
        public bool IsTaxable { get; set; }

    }
}