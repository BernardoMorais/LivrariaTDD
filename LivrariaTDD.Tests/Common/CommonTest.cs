using System.Web.Mvc;
using LivrariaTDD.Controllers;
using LivrariaTDD.Infrastructure.BRL;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Common
{
    [TestFixture]
    public class CommonTest
    {
        [Test]
        public void TodosControlesDevemHerdarDeController()
        {
            /* Neste caso não foi possível testas utlizando mocks, pois a interface não pode herdar da classe "Controller" */
            var homeController = new HomeController();
            var listagemDeProdutosController = new ListagemDeProdutosController(new Mock<IListagemDeProdutosBusiness>().Object);

            Assert.IsInstanceOf(typeof(Controller), homeController);
            Assert.IsInstanceOf(typeof(Controller), listagemDeProdutosController);
        }
    }
}