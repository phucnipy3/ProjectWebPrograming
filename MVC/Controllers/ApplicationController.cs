using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace MVC.Controllers
{
    public abstract class ApplicationController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = UserHelper.GetPropertyValue(HttpContext.User.Identity.Name, x => x.Name);
                ViewBag.UserAvatar = UserHelper.GetPropertyValue(HttpContext.User.Identity.Name, x => x.Image);
            }
        }
    }
}