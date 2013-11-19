using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL.Livro
{
    public partial class ProductBusiness
    {
        public Product SalvarLivro(Product novoProduto)
        {
            return _repository.SalvarLivro(novoProduto);
        }
    }
}
