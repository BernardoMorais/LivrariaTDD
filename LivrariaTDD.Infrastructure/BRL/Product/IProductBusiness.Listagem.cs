using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.BRL.Product
{
    public partial interface IProductBusiness
    {
        List<Models.Product> GetActiveProducts();
        List<Models.Product> GetAll();
    }
}
