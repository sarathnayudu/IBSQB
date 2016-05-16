using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.HTMLHelpers
{
    public static class InputHelper
    {
        public static IHtmlString ValuedCheckBox(this HtmlHelper helper, string name, string value)
        {
            string html = @"<input type=""checkbox"" name=""{0}"" value=""{1}""/>";
            return helper.Raw(String.Format(html, name, value));
        }
    }
}