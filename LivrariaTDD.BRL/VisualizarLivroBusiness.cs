using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL
{
    public class VisualizarLivroBusiness : IVisualizarLivroBusiness
    {
        private IProdutoRepository _repository;

        public VisualizarLivroBusiness(IProdutoRepository produtoRepository)
        {
            _repository = produtoRepository;
        }

        public IProduto RecuperarInformacoesDoLivro(int id)
        {
            return _repository.RecuperarInformacoesDoLivro(id);
        }

        public bool AlterarLivro(int idPrduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            throw new System.NotImplementedException();
        }
    }
}