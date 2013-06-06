using System.Collections.Generic;
using System.Linq;
using LivrariaTDD.DAL.Models;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ILivrariaTDDContext _context;

        public ProdutoRepository(ILivrariaTDDContext context)
        {
            _context = context;
        }

        public List<IProduto> RecuperarTodosProdutos()
        {
            var query = _context.Produtos.Select(produto => produto);

            return query.ToList();
        }

        public IProduto RecuperarInformacoesDoLivro(int id)
        {
            var query = _context.Produtos.Where(x => x.IdProduto == id);

            return query.FirstOrDefault();
        }

        public bool AlterarLivro(int idProduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            var produtoAntigo = _context.Produtos.First(x => x.IdProduto == idProduto);
            produtoAntigo.Nome = nome;
            produtoAntigo.Autor = autor;
            produtoAntigo.Editora = editora;
            produtoAntigo.Ano = ano;
            produtoAntigo.Categoria = categoria;
            produtoAntigo.Estoque = estoque;
            produtoAntigo.Preco = preco;
            produtoAntigo.Foto = foto;

            _context.SaveChanges();
            return true;
        }

        public bool SalvarLivro(string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            var produto = new Produto
                {
                    Nome = nome,
                    Autor = autor,
                    Editora = editora,
                    Ano = ano,
                    Categoria = categoria,
                    Estoque = estoque,
                    Preco = preco,
                    Foto = foto
                };

            _context.GetProdutos().Add(produto);

            return true;
        }
    }
}