using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Infrastructure.Models;
using LivrariaTDD.Models;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController
    {
        public ViewResult CadastrarLivro()
        {
            Helpers.CarregarDadosUsuario(ViewData);

            return View("CadastrarLivro");
        }

        public ViewResult CadastrarLivro(Models.Product.Product novoLivro)
        {
            var produto = new Product();
            produto.InjectFrom(novoLivro);
            var result = _business.SalvarLivro(produto);

            if(result != null)
            {
                ViewData["Sucesso"] = "O produto foi salvo com sucesso!";
                return View("ListagemDeProdutos");
            }
            ViewData["Falha"] = "Ocorreu um erro ao tentar salvar o produto! Tente novamente.";
            return View("CadastrarLivro", novoLivro);
        }
    }
}