using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.BRL.Livro;

namespace LivrariaTDD.Module
{
    public class BRLModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ILivroBusiness>().To<LivroBusiness>();
        }
    }
}
