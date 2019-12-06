using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC.Areas.Admin.Models;
using MVC.Controllers;
using MVC.Models;

namespace MVC.Areas.Admin.Controllers
{
    public class LoginController : ApplicationController
    {
        // GET: Admin/Login
        public ActionResult Login(string ReturnUrl)
        {
            if (String.IsNullOrEmpty(ReturnUrl))
                ReturnUrl = "/Admin";
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminViewModel model, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                if (UserHelper.IsValidUser(model.Username,model.Password))
                {
                    if(HttpContext.User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.SignOut();
                    }
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return Redirect(ReturnUrl);
                }
                ModelState.AddModelError("", "Wrong information, Employee please contact to Administrator when forgot infomation!");
            }
            
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Admin/Login/Login");
        }
    }
}