using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.Models
{
    public interface IPedido
    {
        int IdPedido { get; set; }
        decimal ValorCompra { get; set; }
        decimal ValorFrete { get; set; }
        decimal ValorTotal { get; set; }

        IUsuario Usuario { get; set; }
        IFormaDePagamento FormaDePagamento { get; set; }
        ICollection<IProduto> Produtos { get; set; }
    }
}
