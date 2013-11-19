using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models.Account;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountBusiness _business;        

        public AccountController(IAccountBusiness business)
        {
            _business = business;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            var result = _business.CheckUser(model.Email, Helpers.ConvertToSHA1(model.Password));
            if(result)
            {
                FormsAuthentication.SetAuthCookie(model.Email,true);
                return RedirectToAction("Index", "Home");
            }

            ViewData["erroLogin"] = "Dados do usuário inválidos.";
            return View("Login",new Login());
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            var oldPassword = model.Password;
            model.Password = Helpers.ConvertToSHA1(model.Password);
            var user = new Infrastructure.Models.User();
            user.InjectFrom(model);
            var result = _business.SaveUser(user);
            if(result)
            {
                model.Password = oldPassword;
                var login = new Login();
                login.InjectFrom(model);
                return Login(login);
            }
            return View(model);
        }

        public ActionResult Wishlist()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public JsonResult CheckEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if(_business.CheckEmail(email))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
