namespace LivrariaTDD.Infrastructure.BRL.Livro
{
    public partial interface ILivroBusiness
    {
        bool SalvarLivro(string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto);
    }
}
