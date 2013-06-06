namespace LivrariaTDD.BRL.Livro
{
    public partial class LivroBusiness
    {
        public bool SalvarLivro(string nome, string autor, string editora, int ano, string categoria, int estoque, decimal preco, string foto)
        {
            return _repository.SalvarLivro(nome, autor, editora, ano, categoria, estoque, preco, foto);
        }
    }
}
