using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class IBSSignUP:ModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConformPassWord { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
    }
}