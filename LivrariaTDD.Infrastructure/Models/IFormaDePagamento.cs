using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.Models
{
    public interface IFormaDePagamento
    {
        int IdFormaDePagamento { get; set; }
        string NomeFormaDePagamento { get; set; }
        string Icone { get; set; }

        ICollection<IPedido> Pedidos { get; set; }
    }
}
