using System.Globalization;
using System.IO;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Models.Product;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Admin
{
    public partial class AdminController
    {
        [HttpGet]
        [Authorize (Roles = "admin")]
        public ActionResult Edit(int productId)
        {
            var product = _productBusiness.GetInfo(productId);

            var model = new Product();

            model.InjectFrom(product);

            return View("NewProduct", model);
        }
    }
}
