using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace IntuitSampleMVC.Filters
{
    public class AutoQBLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (WebSecurity.HasUserId)
            {

            }
          

            base.OnActionExecuting(filterContext);
        }
    }
}