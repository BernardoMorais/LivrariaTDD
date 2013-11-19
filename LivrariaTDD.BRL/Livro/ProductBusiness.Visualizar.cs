using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL.Livro
{
    public partial class ProductBusiness
    {
        public Product GetInfo(int id)
        {
            return _repository.RecuperarInformacoesDoLivro(id);
        }

        public bool Update(Product novoProduto)
        {
            return _repository.AlterarLivro(novoProduto);
        }
    }
}