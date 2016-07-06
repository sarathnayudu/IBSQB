using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.Models
{
    public class IBSSignUP:ModelBase
    {
        public int UserID { get; set; }
     
        public string Name { get; set; }
        [Required(ErrorMessage = "An Email Title is required")]
        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "An Password is required")]
        public string Password { get; set; }

       
        [CompareAttribute("Password", ErrorMessage = "Password mismatch")]
        public string ConformPassWord { get; set; }
      
        public string CompanyName { get; set; }
       
        public string Country { get; set; }

        public QBParam QBParamObj { get; set; }

        public bool isLayout { get; set; }
    }
    public class QBParam
    {
        public string AccesKey { get; set; }
        public string AccesSecret { get; set; }
        public string DataSource { get; set; }
        public string Releam { get; set; }
        public string QBEmail { get; set; }
        public string QBCompanyName { get; set; }
    }
}