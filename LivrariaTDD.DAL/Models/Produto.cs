using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Models
{
    public class Produto : IProduto
    {
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int Ano { get; set; }
        public string Categoria { get; set; }
        public int Estoque { get; set; }
        public double Preco { get; set; }
        public string Foto { get; set; }
    }
}
