using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNR.Web
{
    public class NLogExceptionHandlerAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.Write(filterContext.Exception.Message);
            // log error to NLog
            base.OnException(filterContext);
        }
    }
}