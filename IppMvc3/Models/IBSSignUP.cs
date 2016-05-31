using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class IBSSignUP:ModelBase
    {
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
    }
}