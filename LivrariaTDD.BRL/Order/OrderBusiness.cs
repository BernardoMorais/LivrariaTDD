using System.Collections.Generic;
using LivrariaTDD.Infrastructure.BRL.Order;
using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.BRL.Order
{
    public class OrderBusiness : IOrderBusiness
    {
        private IOrderRepository _repository;

        public OrderBusiness(IOrderRepository pedidoRepository)
        {
            _repository = pedidoRepository;
        }

        public bool SalvarPedido(Infrastructure.Models.Order pedido, int userId, List<int> listaIdProdutos, int idFormaDePagamento)
        {
            return _repository.SalvarPedido(pedido, userId, listaIdProdutos, idFormaDePagamento);
        }
    }
}