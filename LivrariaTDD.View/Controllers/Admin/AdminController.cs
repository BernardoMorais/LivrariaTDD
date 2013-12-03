using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Models.Product;
using Omu.ValueInjecter;
using System.Linq;

namespace LivrariaTDD.Controllers.Admin
{
    public partial class AdminController : Controller
    {
        private readonly IAccountBusiness _business;
        private readonly IProductBusiness _productBusiness;

        public AdminController(IAccountBusiness business)
        {
            _business = business;
            _productBusiness = DependencyResolver.Current.GetService<IProductBusiness>();
        }

        public AdminController(IAccountBusiness business, IProductBusiness productBusiness)
        {
            _business = business;
            _productBusiness = productBusiness;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var products = _productBusiness.GetAll();

            var model = new ProductList { Products = products.Select(x => new Models.Product.Product().InjectFrom(x)).Cast<Models.Product.Product>().ToList() };

            return View("Index", model);
        }

    }
}
