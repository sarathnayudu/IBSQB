using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NNR.Web.BLogic
{
    public class QuickBookBlogic
    {
        public QbUser GetQbuser(string qbEmail)
        {
            QBEntities qe = new QBEntities();
            QbUser qbusr = qe.QbUsers.Where(e => e.Email == qbEmail).FirstOrDefault();
            return qbusr;
        }

        public void RegisterUserQBUser(string userName, string qbUsrEmail)
        {
            QBEntities qe = new QBEntities();
           AspNetUser usr= qe.AspNetUsers.Where(e => e.UserName == userName).FirstOrDefault();
           QbUser qbusr= qe.QbUsers.Where(e => e.Email == qbUsrEmail).FirstOrDefault();
            if (usr != null && qbusr != null)
            {
                qe.UserQbUsers.Add(new UserQbUser
                {
                    UserId=usr.Id,
                    QbUserId=qbusr.ID
                });
                qe.SaveChanges();
            }
        }

        public int GetLatestUser(string qbUsrEmail)
        {
            QBEntities qe = new QBEntities();
           UserQbUser usrQBusr= qe.UserQbUsers.OrderByDescending(e => e.Id).FirstOrDefault();
            return usrQBusr != null ? usrQBusr.Id : 0;
        }

        public QbUser GetQbUserbyUserEmail(string userEmail)
        {
            QBEntities qe = new QBEntities();
            UserQbUser usrQbUsr = qe.UserQbUsers.Where(e => e.AspNetUser.Email == userEmail).FirstOrDefault();
            QbUser qbusr=null;
            if (usrQbUsr != null)
            {
                 qbusr = qe.QbUsers.Where(e => e.ID == usrQbUsr.QbUserId).FirstOrDefault();
            }
            return qbusr;
        }
    }
}