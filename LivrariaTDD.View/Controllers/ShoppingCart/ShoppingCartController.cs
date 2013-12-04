using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivrariaTDD.BRL.Account;
using LivrariaTDD.BRL.Livro;
using LivrariaTDD.BRL.Order;
using LivrariaTDD.Infrastructure.Models;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.ShoppingCart
{
    public class ShoppingCartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        public ActionResult FinishOrder()
        {
            var paymentType = int.Parse(HttpContext.Request["paymentType"]);

            var accountService = DependencyResolver.Current.GetService<AccountBusiness>();

            var orderSerice = DependencyResolver.Current.GetService<OrderBusiness>();

            var user = accountService.GetByEmail(HttpContext.User.Identity.Name);

            var total = 0.0M;

            var shoppingCart = (Dictionary<Models.Product.Product, int>)Session["ShoppingCart"] ??
                               new Dictionary<Models.Product.Product, int>();

            var listaProdutos = new List<int>();

            foreach (var item in shoppingCart)
            {
                listaProdutos.Add(item.Key.ProductId);
                total += item.Value*item.Key.Price;
            }

            var order = new Order
                            {PaymentTypeId = paymentType, UserId = user.UserId, OrderValue = total, TotalValue = total};

            orderSerice.SalvarPedido(order, user.UserId, listaProdutos, paymentType);

            Session["ShoppingCart"] = new Dictionary<Models.Product.Product, int>();

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
