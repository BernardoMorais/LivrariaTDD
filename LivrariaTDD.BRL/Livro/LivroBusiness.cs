using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.BRL.Livro
{
    public partial class LivroBusiness
    {
        private readonly IProdutoRepository _repository;
        
        public LivroBusiness(IProdutoRepository produtoRepository)
        {
            _repository = produtoRepository;
        }
    }
}
