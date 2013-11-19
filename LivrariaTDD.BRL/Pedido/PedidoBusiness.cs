using System;
using System.Collections.Generic;
using LivrariaTDD.Infrastructure.BRL.Pedido;
using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.BRL.Pedido
{
    public class PedidoBusiness : IPedidoBusiness
    {
        private IOrderRepository _repository;

        public PedidoBusiness(IOrderRepository pedidoRepository)
        {
            _repository = pedidoRepository;
        }

        public bool SalvarPedido(Infrastructure.Models.Order pedido, int userId, List<int> listaIdProdutos, int idFormaDePagamento)
        {
            return _repository.SalvarPedido(pedido, userId, listaIdProdutos, idFormaDePagamento);
        }

        public decimal CalcularFrete(int countLivros)
        {
            return countLivros*5.0M;
        }
    }
}