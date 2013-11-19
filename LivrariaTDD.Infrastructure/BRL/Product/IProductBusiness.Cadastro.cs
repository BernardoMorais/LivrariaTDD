namespace LivrariaTDD.Infrastructure.BRL.Product
{
    public partial interface IProductBusiness
    {
        Models.Product SalvarLivro(Models.Product novoProduto);

        bool ExcluirLivro(int idLivro);
    }
}
