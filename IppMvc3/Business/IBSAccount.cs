using IntuitSampleMVC.Entity;
using IntuitSampleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IntuitSampleMVC.Business
{
    public class IBSAccount:BusinessBase
    {
        public void Register()
        {
            
        }
        public void Login()
        {
            
        }
        public IBSSignUP GetUserByID(string userName)
        {
            IBSSignUP temp = new IBSSignUP();
            ibshr121414Entities entity = new ibshr121414Entities();
            UserProfile uf= entity.UserProfiles.Where(e => e.Email == userName).FirstOrDefault();
            if (uf != null)
            {
                temp.Email = uf.Email;
                temp.Name = uf.UserName;

            }
            return temp;
        }
        public void GetUserRoles()
        {
        }

        public void GetUserByNameEmail(IBSSignUP registerModel)
        {
            ibshr121414Entities entity = new ibshr121414Entities();
          UserProfile uf=  entity.UserProfiles.Where(e => e.UserName == registerModel.Name && 
              e.Email==registerModel.Email).FirstOrDefault();
          if (uf != null)
          {
              registerModel.CompanyName = uf.CompanyName;
              registerModel.Country = uf.Country;
              registerModel.PhoneNumber = uf.PhoneNumber;
          }

        }
    }
}