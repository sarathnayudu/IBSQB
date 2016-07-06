using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntuitSampleMVC.Models;

namespace IntuitSampleMVC.Controllers
{
    public class BaseController : Controller
    {
        QBUser qbusr;
      

        public QBUser QBUser
        {
            get
            {
                qbusr = (QBUser)Session["QBUser"];
                if (qbusr == null)
                    qbusr = new QBUser();
                return qbusr;
            }
            set 
            {
                Session["QBUser"] = value;
            }
        } 
    }
}
