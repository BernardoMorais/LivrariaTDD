using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController
    {
        public ViewResult ExcluirLivro(int idLivro)
        {
            Helpers.CarregarDadosUsuario(ViewData);

            if(_business.ExcluirLivro(idLivro))
            {
                ViewData["Sucesso"] = "O livro foi excluído com sucesso.";
                return View("ListagemDeProdutos");
            }

            ViewData["Erro"] = "O livro não foi excluído com sucesso. Por gentileza tente novamente.";
            return View("ListagemDeProdutos");
        }
    }
}