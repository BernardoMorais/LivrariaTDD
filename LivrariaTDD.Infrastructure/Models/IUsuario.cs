using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.Models
{
    public interface IUsuario
    {
        int IdUsuario { get; set; }
        string Email { get; set; }
        string Senha { get; set; }
        string Nome { get; set; }
        string Rua { get; set; }
        int Numero { get; set; }
        string Bairro { get; set; }
        string Cidade { get; set; }
        string Estado { get; set; }
        int CEP { get; set; }
        string Telefone { get; set; }
        string Celular { get; set; }
        string TipoUsuario { get; set; }

        ICollection<IPedido> Pedidos { get; set; }
    }
}
