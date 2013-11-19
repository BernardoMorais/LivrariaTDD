using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Repository
{
    public interface IOrderRepository
    {
        bool SalvarPedido(Order pedido, int userId, List<int> listaIdProdutos, int formaPagamentoId);
    }
}