using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models.Product;
using Omu.ValueInjecter;
using System.Linq;

namespace LivrariaTDD.Controllers.Home
{
    public partial class HomeController : Controller, IHomeController
    {
        private readonly IProductBusiness _business;

        public HomeController()
        {
        }

        public HomeController(IProductBusiness business)
        {
            _business = business;
        }

        public ActionResult Index()
        {
            try
            {
                var products = _business.GetActiveProducts();

                var model = new ProductList
                {
                    Products =
                        products != null
                            ? products.Select(x => new Models.Product.Product().InjectFrom(x)).Cast<Models.Product.Product>().ToList()
                            : new List<Models.Product.Product>()
                };

                return View("Index", model);
            }
            catch (Exception)
            {
                ViewData["Erro"] = "Ocorreu um erro durante o processamento. Tente novamente mais tarde.";

                var model = new ProductList
                {
                    Products = new List<Models.Product.Product>()
                };

                return View("Index", model);
            }
        }
    }
}