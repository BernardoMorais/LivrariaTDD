using System.Web.Mvc;
using LivrariaTDD.Infrastructure.View.Controllers;

namespace LivrariaTDD.Controllers
{
    public class HomeController : Controller, IHomeController
    {
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("Index","ListagemDeProdutos");
        }
    }
}