using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.BRL
{
    public interface IListagemDeProdutosBusiness
    {
        List<IProduto> RecuperarTodosProdutos();
    }
}
