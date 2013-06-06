using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Livro;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController : Controller
    {
        private readonly ILivroBusiness _business;

        public LivroController(ILivroBusiness livroBusiness)
        {
            _business = livroBusiness;
        }
    }
}