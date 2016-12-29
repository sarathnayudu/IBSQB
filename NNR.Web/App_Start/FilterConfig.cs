using System.Web;
using System.Web.Mvc;

namespace NNR.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NLogExceptionHandlerAttribute());
        }
    }
}
