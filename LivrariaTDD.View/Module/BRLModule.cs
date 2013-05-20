using LivrariaTDD.BRL;
using LivrariaTDD.Infrastructure.BRL;

namespace LivrariaTDD.Module
{
    public class BRLModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IListagemDeProdutosBusiness>().To<ListagemDeProdutosBusiness>();
        }
    }
}
