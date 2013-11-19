namespace LivrariaTDD.Infrastructure.BRL.Product
{
    public partial interface IProductBusiness
    {
        LivrariaTDD.Infrastructure.Models.Product GetInfo(int id);
        bool Update(LivrariaTDD.Infrastructure.Models.Product novoProduto);
    }
}