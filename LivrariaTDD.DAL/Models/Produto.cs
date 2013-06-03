using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Models
{
    public class Produto : IProduto
    {
        public int IdPrduto { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int Ano { get; set; }
        public string Categoria { get; set; }
        public int Estoque { get; set; }
        public decimal Preco { get; set; }
        public string Foto { get; set; }

        public virtual ICollection<IPedido> Pedidos { get; set; }
    }
}
