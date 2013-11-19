using System.Web.Mvc;
using System.Web.Security;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models.Account;

namespace LivrariaTDD.Controllers.Admin
{
    public partial class AdminController
    {
        [HttpGet]
        public ActionResult Login()                                         
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var result = _business.CheckUser(model.Email, Helpers.ConvertToSHA1(model.Password));
            if (result)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("Index");
            }

            return View("Login");
        }

    }
}
