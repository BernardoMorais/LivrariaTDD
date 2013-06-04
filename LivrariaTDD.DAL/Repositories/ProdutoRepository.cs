using System.Collections.Generic;
using System.Linq;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private ILivrariaTDDContext _context;

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
            var query = _context.Produtos.Where(x => x.IdPrduto == id);

            return query.FirstOrDefault();
        }
    }
}