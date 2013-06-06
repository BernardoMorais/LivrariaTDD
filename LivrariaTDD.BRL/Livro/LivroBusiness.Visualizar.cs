using LivrariaTDD.Infrastructure.BRL.Livro;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL.Livro
{
    public partial class LivroBusiness : ILivroBusiness
    {
        public IProduto RecuperarInformacoesDoLivro(int id)
        {
            return _repository.RecuperarInformacoesDoLivro(id);
        }

        public bool AlterarLivro(int idPrduto, string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            return _repository.AlterarLivro(idPrduto, nome, autor, editora, ano, categoria, estoque, preco, foto);
        }
    }
}