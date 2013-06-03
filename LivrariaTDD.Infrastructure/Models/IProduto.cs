using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.Models
{
    public interface IProduto
    {
        int IdPrduto { get; set; }
        string Nome { get; set; }
        string Autor { get; set; }
        string Editora { get; set; }
        int Ano { get; set; }
        string Categoria { get; set; }
        int Estoque { get; set; }
        decimal Preco { get; set; }
        string Foto { get; set; }
    
        ICollection<IPedido> Pedidos { get; set; }
    }
}