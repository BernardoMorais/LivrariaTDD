using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaTDD.BRL;
using LivrariaTDD.Infrastructure.BRL;

namespace LivrariaTDD.DependencyInjection.Modules
{
    public class BRLModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IListagemDeProdutosBusiness>().To<ListagemDeProdutosBusiness>();
        }
    }
}
