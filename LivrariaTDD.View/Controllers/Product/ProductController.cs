using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;

namespace LivrariaTDD.Controllers.Product
{
    public partial class ProductController : Controller
    {
        private readonly IProductBusiness _business;

        public ProductController(IProductBusiness productBusiness)
        {
            _business = productBusiness;
        }
    }
}