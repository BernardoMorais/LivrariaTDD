using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Models
{
    public class Pedido : IPedido
    {
        public int IdPedido { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorTotal { get; set; }

        public IUsuario Usuario { get; set; }
        public IFormaDePagamento FormaDePagamento { get; set; }
        public ICollection<IProduto> Produtos { get; set; }
    }
}
