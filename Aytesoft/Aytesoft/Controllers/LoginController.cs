using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models.View;
using Aytesoft.Models.Edit;
using System.Web.Security;
using System.Web.SessionState;
using Services.Interfaces;
using Services;

namespace Aytesoft.Controllers
{
    public class LoginController : Controller
    {
        ILoginService _LoginService;
        public LoginController(ILoginService LoginService)
        {
            _LoginService = LoginService;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(UserEdit user)
        {
            if(ModelState.IsValid)
            {
                UserView auth = _LoginService.Authorization(user);
                if (auth.UserName != null)
                {
                    TempData["Name"] = auth.Name.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}