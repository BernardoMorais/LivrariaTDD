using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.BRL.Order
{
    public interface IOrderBusiness
    {
        bool SalvarPedido(Models.Order pedido, int userId, List<int> listaIdProdutos, int idFormaDePagamento);
    }
}