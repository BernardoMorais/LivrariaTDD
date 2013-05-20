using System.Collections.Generic;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL
{
    public class ListagemDeProdutosBusiness : IListagemDeProdutosBusiness
    {
        private IProdutoRepository _produtoRepository;

        public ListagemDeProdutosBusiness(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public List<IProduto> RecuperarTodosProdutos()
        {
            return _produtoRepository.RecuperarTodosProdutos();
        }
    }
}