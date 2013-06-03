using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Models
{
    public class Usuario : IUsuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int CEP { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }

        public ICollection<IPedido> Pedidos { get; set; }
    }
}
