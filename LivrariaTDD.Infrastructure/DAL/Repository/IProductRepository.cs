using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Repository
{
    public interface IProductRepository
    {
        List<Product> GetActiveProducts();
        List<Product> RecuperarTodosProdutos();

        Product RecuperarInformacoesDoLivro(int id);
        bool AlterarLivro(Product novoProduto);
        Product SalvarLivro(Product novoProduto);
        bool ExcluirLivro(int produtoId);
    }
}