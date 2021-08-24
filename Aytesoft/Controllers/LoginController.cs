using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models;
using Aytesoft.DataAccessLayer;
using System.Web.Security;
using System.Web.SessionState;

namespace Aytesoft.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            User auth = ProductDB.getUserWithLogin(user.username, user.password);
            if(auth.password != null && auth.username != null)
            {
                FormsAuthentication.SetAuthCookie(auth.id.ToString(), false);
                TempData["Name"] = auth.name.ToString();
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