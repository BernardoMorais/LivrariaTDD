using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Models;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers
{
    public class VisualizarLivroController : Controller
    {
        private readonly IVisualizarLivroBusiness _business;

        public VisualizarLivroController(IVisualizarLivroBusiness visualizarLivroBusiness)
        {
            _business = visualizarLivroBusiness;
        }

        public ViewResult VisualizarLivro(int id)
        {
            if (ViewData.ContainsKey("logado"))
                ViewData["logado"] = ViewData["logado"];
            else
                ViewData.Add("logado", "");

            if (ViewData.ContainsKey("tipoUsuario"))
                ViewData["tipoUsuario"] = ViewData["tipoUsuario"];
            else
                ViewData.Add("tipoUsuario", "");

            if (ViewData.ContainsKey("erroLogin"))
                ViewData["erroLogin"] = ViewData["erroLogin"];
            else
                ViewData.Add("erroLogin", "");

            var produto = _business.RecuperarInformacoesDoLivro(id);

            var produtoModel = new ProdutoModel();

            produtoModel.InjectFrom(produto);

            ViewData["livro"] = produtoModel;

            return View("VisualizarLivro");
        }

        public ViewResult AlterarLivro(int idPrduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            if (ViewData.ContainsKey("logado"))
                ViewData["logado"] = ViewData["logado"];
            else
                ViewData.Add("logado", "");

            if (ViewData.ContainsKey("tipoUsuario"))
                ViewData["tipoUsuario"] = ViewData["tipoUsuario"];
            else
                ViewData.Add("tipoUsuario", "");

            if (ViewData.ContainsKey("erroLogin"))
                ViewData["erroLogin"] = ViewData["erroLogin"];
            else
                ViewData.Add("erroLogin", "");

            var result = _business.AlterarLivro(idPrduto, nome, autor, editora, ano, categoria, estoque, preco, foto);

            ViewData["livro"] = new ProdutoModel()
                {
                    IdPrduto = idPrduto,
                    Nome = nome,
                    Autor = autor,
                    Editora = editora,
                    Ano = ano,
                    Categoria = categoria,
                    Estoque = estoque,
                    Preco = preco,
                    Foto = foto
                };

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