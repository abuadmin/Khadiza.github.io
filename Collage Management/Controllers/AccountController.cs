using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Collage_Management.Models;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace Collage_Management.Controllers
{
    public class AccountController : Controller
    {
        private School_ManagementEntities db = new School_ManagementEntities();
        // GET: Account

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [Email(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }
        public ActionResult Login(User u)
        {
            School_ManagementEntities db = new School_ManagementEntities();
            var count = db.Users.Where(x => x.UserName == u.UserName && x.Password == u.Password).Count();
            if (count == 0)
            {
               
                    ViewBag.msg = "";
                    return View();
                }
                else
          
            {
               
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                
                return RedirectToAction("Index", "Users");
             

            }
        }

        public JsonResult CheckUserName(string userName)
        {
            var user = db.Users.Where(a => a.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login");
        }
        }
    }
