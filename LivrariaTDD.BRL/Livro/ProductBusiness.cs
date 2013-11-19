using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.BRL.Livro
{
    public partial class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _repository;
        
        public ProductBusiness(IProductRepository produtoRepository)
        {
            _repository = produtoRepository;
        }
    }
}
