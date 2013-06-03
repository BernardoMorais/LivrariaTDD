using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Models
{
    public class FormaDePagamento : IFormaDePagamento
    {
        public int IdFormaDePagamento { get; set; }
        public string NomeFormaDePagamento { get; set; }
        public string Icone { get; set; }

        public ICollection<IPedido> Pedidos { get; set; }
    }
}
