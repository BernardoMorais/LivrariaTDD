using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.BRL
{
    public interface IVisualizarLivroBusiness
    {
        IProduto RecuperarInformacoesDoLivro(int id);
        bool AlterarLivro(int idPrduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto);
    }
}