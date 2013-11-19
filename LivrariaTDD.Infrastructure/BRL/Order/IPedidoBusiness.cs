using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.BRL.Pedido
{
    public interface IPedidoBusiness
    {
        bool SalvarPedido(Models.Order pedido, int userId, List<int> listaIdProdutos, int idFormaDePagamento);
        decimal CalcularFrete(int countLivros);
    }
}