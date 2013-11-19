using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.Module
{
    public class RepositoryModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IProductRepository>().To<ProdutoRepository>();
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IOrderRepository>().To<PedidoRepository>();
        }
    }
}