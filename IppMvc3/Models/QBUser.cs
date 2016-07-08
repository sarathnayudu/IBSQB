using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntuitSampleMVC.Models
{
    public class QBUser
    {
        public string AccesKey { get; set; }
        public string AccesSecret { get; set; }
        public string DataSource { get; set; }
        public string Releam { get; set; }
        public string QBEmail { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string  PhoneNumber { get; set; }

        public bool OpenIdResponse { get; set; }
        public string Mobile { get; set; }
      
        public bool IsShowConnect
        {
            get { return !validateQBfields(); }
          
        }       
        
     private bool  validateQBfields()
        {
            return (!string.IsNullOrEmpty(AccesKey) && !string.IsNullOrEmpty(AccesSecret) && !string.IsNullOrEmpty(Releam) && !string.IsNullOrEmpty(DataSource));
        }
        
    }
}