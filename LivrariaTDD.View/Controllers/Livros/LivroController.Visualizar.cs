using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController
    {
        public ViewResult VisualizarLivro(int id)
        {
            Helpers.CarregarDadosUsuario(ViewData);

            var produto = _business.RecuperarInformacoesDoLivro(id);

            var produtoModel = new ProdutoModel();

            produtoModel.InjectFrom(produto);

            ViewData["livro"] = produtoModel;

            return View("VisualizarLivro");
        }

        public ViewResult AlterarLivro(int idProduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            Helpers.CarregarDadosUsuario(ViewData);

            var result = _business.AlterarLivro(idProduto, nome, autor, editora, ano, categoria, estoque, preco, foto);

            ViewData["livro"] = new ProdutoModel
                {
                    IdProduto = idProduto,
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