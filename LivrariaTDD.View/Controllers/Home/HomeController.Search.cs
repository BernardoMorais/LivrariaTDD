using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models.Product;
using Omu.ValueInjecter;
using System.Linq;

namespace LivrariaTDD.Controllers.Home
{
    public partial class HomeController
    {
        public ViewResult Search(string name, Categories? categoty)
        {
            var lista = _business.GetActiveProducts();

            var model = new ProductList();

            if(lista != null)
            {
                if (!String.IsNullOrEmpty(name) && categoty != null)
                    model.Products =
                        lista.Where(
                            x =>
                            ((!String.IsNullOrEmpty(name) && x.Name.ToLower().Contains(name.ToLower())) && (x.Category.Equals(categoty)))).
                            Select(x => new Models.Product.Product().InjectFrom(x)).Cast<Models.Product.Product>().ToList();
                else if (!String.IsNullOrEmpty(name))
                    model.Products =
                        lista.Where(x => !String.IsNullOrEmpty(name) && x.Name.ToLower().Contains(name.ToLower())).Select(
                            x => new Models.Product.Product().InjectFrom(x)).Cast<Models.Product.Product>().ToList();
                else if (categoty != null)
                    model.Products =
                        lista.Where(x => x.Category.Equals(categoty)).Select(x => new Models.Product.Product().InjectFrom(x)).Cast
                            <Models.Product.Product>().ToList();
                else
                    model.Products = lista.Select(x => new Models.Product.Product().InjectFrom(x)).Cast<Models.Product.Product>().ToList();
            }
            else
            {
                model.Products = new List<Models.Product.Product>();
            }

            return View("Index",model);
        }
    }
}