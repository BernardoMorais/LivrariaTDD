using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Models
{
    public class ProdutoModel
    {
        public string Nome { get; set; }

        public string Autor { get; set; }

        public string Editora { get; set; }

        public int Ano { get; set; }

        public string Categoria { get; set; }

        public int Estoque { get; set; }

        public decimal Preco { get; set; }

        public string Foto { get; set; }

        public int IdProduto { get; set; }
    }
}