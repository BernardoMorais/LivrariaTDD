using LivrariaTDD.Controllers;
using LivrariaTDD.Infrastructure.View.Controllers;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Home
{
    [TestFixture]
    public class HomeControllerTest
    {
        private IHomeController _controller;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _controller = new HomeController();
        }

        [Test]
        public void QuandoAcessoOEnderecoPricipalDoSite_ComoFuncionarioDaLoja_APaginaDeListagemDeProdutosEAcessada()
        {
            #region Passo1
            //var controller = new Mock<IHomeController>();

            //controller.Setup(x => x.Index()).Returns(new RedirectToRouteResult(new RouteValueDictionary{ {"controller", "ListagemDeProdutos"}, {"action", "Index"} }));

            //var result = controller.Object.Index() as RedirectToRouteResult;

            //Assert.Contains("LivroController",result.RouteValues.Values);

            //Assert.Contains("Index", result.RouteValues.Values);
            #endregion

            var result = _controller.Index();

            Assert.Contains("ListagemDeProdutos", result.RouteValues.Values);

            Assert.Contains("Index", result.RouteValues.Values);
        }
    }
}