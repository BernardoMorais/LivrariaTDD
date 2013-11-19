using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Infrastructure.Models;
using LivrariaTDD.Models;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController
    {
        public ViewResult VisualizarLivro(int id)
        {
            Helpers.CarregarDadosUsuario(ViewData);

            var produto = _business.GetInfo(id);

            var produtoModel = new Models.Product.Product();

            produtoModel.InjectFrom(produto);

            ViewData["livro"] = produtoModel;

            return View("VisualizarLivro");
        }

        public ViewResult AlterarLivro(Models.Product.Product novoProduto)
        {
            Helpers.CarregarDadosUsuario(ViewData);

            var produto = new Product();

            produto.InjectFrom(novoProduto);

            var result = _business.Update(produto);

            ViewData["livro"] = novoProduto;

            if(result)
            {
                ViewData["Sucesso"] = "As informações foram gravadas com sucesso.";
            }   
            else
            {
                ViewData["Erro"] = "Ocorreu um erro durante o processamento e não foi possível salvar as alterações.";
            }

            return View("VisualizarLivro");
        }
    }
}