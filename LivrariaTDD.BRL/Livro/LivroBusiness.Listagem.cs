using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL.Livro
{
    public partial class LivroBusiness
    {
        public List<IProduto> RecuperarTodosProdutos()
        {
            return _repository.RecuperarTodosProdutos();
        }
    }
}