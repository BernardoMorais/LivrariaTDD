using System.Web.Routing;
using LivrariaTDD.Controllers;
using LivrariaTDD.Infrastructure.View.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace LivrariaTDD.MVCTests
{
    [TestFixture]
    public class ListagemDeProdutos
    {
        [Test]
        public void QuandoAcessoAPagina_ComoFuncionarioDaLoja_APaginaEstaAcessivel()
        {
            #region Passo1
            /*Inicialmente o Teste foi feito utlizando um objeto Mock, depois sim foi adiconado o controle vadadeiro.*/
             
            //var controller = new Mock<IListagemDeProdutosController>();

            //controller.Setup(x => x.Index()).Returns(new ViewResult());

            //var result = controller.Object.Index() as ViewResult;

            //Assert.AreEqual("", result.ViewName);
            #endregion

            var controller = new ListagemDeProdutosController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("", result.ViewName);
        }

        [Test]
        public void QuandoAcessoOEnderecoPricipalDoSite_ComoFuncionarioDaLoja_APaginaDeListagemDeProdutosEAcessada()
        {
            #region Passo1
            //var controller = new Mock<IHomeController>();

            //controller.Setup(x => x.Index()).Returns(new RedirectToRouteResult(new RouteValueDictionary{ {"controller", "ListagemDeProdutos"}, {"action", "Index"} }));

            //var result = controller.Object.Index() as RedirectToRouteResult;

            //Assert.Contains("ListagemDeProdutosController",result.RouteValues.Values);

            //Assert.Contains("Index", result.RouteValues.Values);
            #endregion

            var controller = new HomeController();

            var result = controller.Index() as RedirectToRouteResult;

            Assert.Contains("ListagemDeProdutos", result.RouteValues.Values);

            Assert.Contains("Index", result.RouteValues.Values);
        }

        [Test]
        public void TodosControlesDevemHerdarDeController()
        {
            /* Neste caso não foi possível testas utlizando mocks, pois a interface não pode herdar da classe "Controller" */
            var homeController = new HomeController();
            var listagemDeProdutosController = new ListagemDeProdutosController();

            Assert.IsInstanceOf(typeof(Controller), homeController);
            Assert.IsInstanceOf(typeof(Controller), listagemDeProdutosController);
        }
    }
}