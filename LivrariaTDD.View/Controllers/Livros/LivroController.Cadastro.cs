using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController
    {
        public ViewResult CadastrarLivro()
        {
            Helpers.CarregarDadosUsuario(ViewData);

            return View("CadastrarLivro");
        }

        public ViewResult CadastrarLivro(ProdutoModel novoLivro)
        {
            var result = _business.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria,
                                  novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            if(result)
            {
                ViewData["Sucesso"] = "O produto foi salvo com sucesso!";
                return View("ListagemDeProdutos");
            }
            ViewData["Falha"] = "Ocorreu um erro ao tentar salvar o produto! Tente novamente.";
            return View("CadastrarLivro", novoLivro);
        }
    }
}