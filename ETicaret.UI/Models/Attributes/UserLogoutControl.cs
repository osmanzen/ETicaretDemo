using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Models.Attributes
{
    public class UserLogoutControl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
                filterContext.HttpContext.Response.Redirect("~/");
            }
        }
    }
}