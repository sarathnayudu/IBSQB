using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class IBSSignUP:ModelBase
    {
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConformPassWord { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Country { get; set; }

        public QBParam QBParamObj { get; set; }

        public bool isLayout { get; set; }
    }
    public class QBParam
    {
        public string AccesKey { get; set; }
        public string AccesSecret { get; set; }
        public string  DataSource { get; set; }
        public string Releam { get; set; }
        public string QBEmail { get; set; }
        public string QBCompanyName { get; set; }
    }
}