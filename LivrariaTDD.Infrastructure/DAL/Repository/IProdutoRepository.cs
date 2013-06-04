using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Repository
{
    public interface IProdutoRepository
    {
        List<IProduto> RecuperarTodosProdutos();

        IProduto RecuperarInformacoesDoLivro(int id);
    }
}