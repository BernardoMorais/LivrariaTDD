using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController : Controller
    {
        private readonly IProductBusiness _business;

        public LivroController(IProductBusiness livroBusiness)
        {
            _business = livroBusiness;
        }
    }
}