using System.Web.Mvc;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Product
{
    public partial class ProductController
    {
        public ViewResult Detail(int id)
        {
            var product = _business.GetInfo(id);

            var productModel = new Models.Product.Product();

            productModel.InjectFrom(product);

            return View("Index", productModel);
        }
    }
}