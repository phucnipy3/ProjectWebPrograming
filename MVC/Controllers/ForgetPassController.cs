using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ForgetPassController : ApplicationController
    {
        private DatabaseDetailsContext db = new DatabaseDetailsContext();
        // GET: ForgetPass
        [HttpPost]
        public ActionResult ForgetPass(ForgetPassViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Content("success");

                var sql = from users in db.Users
                          where users.UserID == model.Email
                          select users;
                if (sql.Count() == 0)
                {
                    ModelState.AddModelError("", "Email not exist!");
                    return PartialView("../Shared/ForgetPass", model);
                }

                var mail = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("minhtantanhiep@gmail.com", "Minhtan@99"),
                    EnableSsl = true
                };

                var masage = new MailMessage();
                masage.From = new MailAddress("minhtantanhiep@gmail.com");
                masage.ReplyToList.Add("minhtantanhiep@gmail.com");
                masage.To.Add(new MailAddress(model.Email));
                masage.Subject = "Forget Pass";
                masage.Body = "We send mail from admin opxin.com. " +
                              "Your account is " + sql.First().UserID + ", and you forgot your password. " +
                              "Your password is " + sql.First().Password + ".";
                masage.IsBodyHtml = true;
                mail.Send(masage);
                return Content("success");
            }
            return PartialView("../Shared/ForgetPass", model);
        }
    }
}