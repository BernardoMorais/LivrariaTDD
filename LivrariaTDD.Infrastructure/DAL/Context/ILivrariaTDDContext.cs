using System.Linq;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Context
{
    public interface ILivrariaTDDContext
    {
        IQueryable<IUsuario> Usuarios { get; }
        IQueryable<IProduto> Produtos { get; }
        IQueryable<IPedido> Pedidos { get; }
        IQueryable<IFormaDePagamento> FormasDePagamento { get; }
    }
}