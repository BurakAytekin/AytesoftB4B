using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models.Domain;
using Aytesoft.DataAccessLayer;
using System.Web.Security;
using System.Web.SessionState;

namespace Aytesoft.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            User auth = ProductDB.GetUserWithLogin(user.UserName, user.Password);
            if(auth.UserName != null && auth.UserName != null)
            {
                FormsAuthentication.SetAuthCookie(auth.ID.ToString(), false);
                TempData["Name"] = auth.Name.ToString();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index","Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}