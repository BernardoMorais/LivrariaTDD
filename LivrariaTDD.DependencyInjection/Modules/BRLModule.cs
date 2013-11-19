using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.BRL.Product;

namespace LivrariaTDD.DependencyInjection.Modules
{
    public class BRLModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IProductBusiness>().To<ProductBusiness>();
        }
    }
}
