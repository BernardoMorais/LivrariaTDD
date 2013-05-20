namespace LivrariaTDD.Infrastructure.Models
{
    public interface IProduto
    {
        string Nome { get; set; }

        string Autor { get; set; }

        string Editora { get; set; }

        int Ano { get; set; }

        string Categoria { get; set; }

        int Estoque { get; set; }

        double Preco { get; set; }

        string Foto { get; set; }
    }
}