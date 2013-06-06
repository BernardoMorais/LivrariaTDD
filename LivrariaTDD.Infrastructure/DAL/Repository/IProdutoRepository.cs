using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Repository
{
    public interface IProdutoRepository
    {
        List<IProduto> RecuperarTodosProdutos();

        IProduto RecuperarInformacoesDoLivro(int id);
        bool AlterarLivro(int idPrduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto);
        bool SalvarLivro(string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto);
    }
}