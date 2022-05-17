using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebAdmin.Models.Login;

namespace WebAdmin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (User)Session["Login"];
            if (session == null)
            {
                session = new User()
                {
                    Uid = "sasdasdads",
                    position = true,
                    UseName = "Truong",
                };
                Session["Login"] = session;
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}