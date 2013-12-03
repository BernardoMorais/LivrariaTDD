using System.Globalization;
using System.IO;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Models.Product;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Admin
{
    public partial class AdminController
    {
        [HttpGet]
        [Authorize (Roles = "admin")]
        public ActionResult Desactive(int productId)
        {
            var product = _productBusiness.GetInfo(productId);

            product.Status = ProductStatus.Inative;

            _productBusiness.Update(product);

            var model = new Models.Product.Product();

            model.InjectFrom(product);

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Active(int productId)
        {
            var product = _productBusiness.GetInfo(productId);

            product.Status = ProductStatus.Active;

            _productBusiness.Update(product);

            var model = new Models.Product.Product();

            model.InjectFrom(product);

            return RedirectToAction("Index", "Admin");
        }
    }
}
