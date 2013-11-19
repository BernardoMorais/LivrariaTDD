using LivrariaTDD.BRL.Account;
using LivrariaTDD.BRL.Livro;
using LivrariaTDD.BRL.Pedido;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.BRL.Pedido;
using LivrariaTDD.Infrastructure.BRL.Product;

namespace LivrariaTDD.Module
{
    public class BRLModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IProductBusiness>().To<ProductBusiness>();
            Bind<IAccountBusiness>().To<AccountBusiness>();
            Bind<IPedidoBusiness>().To<PedidoBusiness>();
        }
    }
}
