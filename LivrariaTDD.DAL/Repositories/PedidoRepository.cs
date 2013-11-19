using System;
using System.Linq;
using System.Collections.Generic;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Repositories
{
    public class PedidoRepository : IOrderRepository
    {
        private LivrariaTDDContext _context;

        public PedidoRepository(LivrariaTDDContext livrariaContext)
        {
            _context = livrariaContext;
        }

        public bool SalvarPedido(Order pedido, int userId, List<int> listaIdProducts, int formaPagamentoId)
        {
            try
            {
                pedido.Products = new List<Product>();

                foreach (var idProduto in listaIdProducts)
                {
                    var produto = _context.Products.FirstOrDefault(x => x.ProductId == idProduto);
                    if (produto != null)
                    {
                        pedido.Products.Add(produto);
                    }
                    else
                    {
                        return false;
                    }
                }

                var usuario = _context.Users.FirstOrDefault(x => x.UserId == userId);
                if(usuario != null)
                {
                    pedido.User = usuario;
                }
                else
                {
                    return false;
                }

                var formaDePagamento =
                    _context.PaymentTypes.FirstOrDefault(x => x.PaymentTypeId == formaPagamentoId);
                if(formaDePagamento != null)
                {
                    pedido.PaymentType = formaDePagamento;
                }
                else
                {
                    return false;
                }

                _context.Orders.Add(pedido);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}