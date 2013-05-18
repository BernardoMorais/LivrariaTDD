using System.Web.Mvc;
using LivrariaTDD.Infrastructure.View.Controllers;

namespace LivrariaTDD.Controllers
{
    public class ListagemDeProdutosController : Controller, IListagemDeProdutosController
    {
        public ViewResult Index()
        {
            return View("ListagemDeProdutos");
        }
    }
}