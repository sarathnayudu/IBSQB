using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class SignUPOrLogin
    {
        public IBSSignUP IBSSignUP { get; set; }
        public LogOnModel LogOnModel { get; set; }
    }
}