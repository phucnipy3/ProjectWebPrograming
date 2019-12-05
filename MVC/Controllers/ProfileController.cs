using MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProfileController : ApplicationController
    {
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            string userName = HttpContext.User.Identity.Name;
            User user = UserHelper.GetUserByUserID(userName);
            return View(user);
        }

        public ActionResult Update(User user, HttpPostedFileBase Avatar)
        {
            if (Avatar != null)
            {
                string fileName = UserHelper.GetUserByUserID(HttpContext.User.Identity.Name).ID.ToString() + DateTime.Now.ToString("-HH-mm-ss-dd-MM-yyyy")+Path.GetExtension(Avatar.FileName);
                user.Image = "Resource/Avatar/" + fileName;
                string savePath = Path.Combine(Server.MapPath("~/Resource/Avatar/"), fileName);
                Avatar.SaveAs(savePath);
            }
            UserHelper.UpdateUser(user);
            return View("Index");
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordInfo model)
        {
            if(ModelState.IsValid)
            {
                if(UserHelper.ChangePassword(HttpContext.User.Identity.Name,model.OldPassword,model.NewPassword))
                {
                    return Content("success");
                }
                else
                {
                    ModelState.AddModelError("","Mật khẩu không đúng");
                    return PartialView("ChangePassword", model);
                    
                }

            }
            return PartialView("ChangePassword", model);
        }

    }
}