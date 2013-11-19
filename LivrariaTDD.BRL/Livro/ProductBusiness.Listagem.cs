using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL.Livro
{
    public partial class ProductBusiness
    {
        public List<Product> GetAll()
        {
            return _repository.RecuperarTodosProdutos();
        }

        public List<Product> GetActiveProducts()
        {
            return _repository.GetActiveProducts();
        }
    }
}