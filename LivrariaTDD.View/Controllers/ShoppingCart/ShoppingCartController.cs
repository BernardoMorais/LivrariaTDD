using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivrariaTDD.BRL.Livro;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.ShoppingCart
{
    public class ShoppingCartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Add(int productId)
        {
            var shoppingCart = (Dictionary<Models.Product.Product, int>)Session["ShoppingCart"] ??
                               new Dictionary<Models.Product.Product, int>();

            var productService = DependencyResolver.Current.GetService<ProductBusiness>();

            var product = new Models.Product.Product();

            var finded = false;

            foreach (var item in shoppingCart)
            {
                if(item.Key.ProductId == productId)
                {
                    product = item.Key;
                    finded = true;
                }
            }

            if(!finded)
            {
                product.InjectFrom(productService.GetInfo(productId));

                shoppingCart.Add(product, 1);
            }
            else
            {
                shoppingCart[product]++;
            }

            Session["ShoppingCart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int productId)
        {
            var shoppingCart = (Dictionary<Models.Product.Product, int>)Session["ShoppingCart"] ??
                               new Dictionary<Models.Product.Product, int>();

            var productService = DependencyResolver.Current.GetService<ProductBusiness>();

            var product = new Models.Product.Product();

            var finded = false;

            foreach (var item in shoppingCart)
            {
                if (item.Key.ProductId == productId)
                {
                    product = item.Key;
                    finded = true;
                }
            }

            if (finded)
            {
                shoppingCart[product]--;

                if (shoppingCart[product] == 0)
                    shoppingCart.Remove(product);
            }

            Session["ShoppingCart"] = shoppingCart;

            return RedirectToAction("Index");
        }

    }
}
