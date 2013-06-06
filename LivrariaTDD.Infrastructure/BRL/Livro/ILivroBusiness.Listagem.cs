using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.BRL.Livro
{
    public partial interface ILivroBusiness
    {
        List<IProduto> RecuperarTodosProdutos();
    }
}
