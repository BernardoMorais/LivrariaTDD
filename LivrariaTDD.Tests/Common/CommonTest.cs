using System.Web.Mvc;
using LivrariaTDD.Controllers.Account;
using LivrariaTDD.Controllers.Home;
using LivrariaTDD.Controllers.Pedido;
using LivrariaTDD.Controllers.Product;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.BRL.Pedido;
using LivrariaTDD.Infrastructure.BRL.Product;
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
            var productController = new ProductController(new Mock<IProductBusiness>().Object);
            var accountController = new AccountController(new Mock<IAccountBusiness>().Object);
            var carrinhoDeComprasController = new CarrinhoDeComprasController(new Mock<IPedidoBusiness>().Object);

            Assert.IsInstanceOf(typeof(Controller), homeController);
            Assert.IsInstanceOf(typeof(Controller), productController);
            Assert.IsInstanceOf(typeof(Controller), accountController);
            Assert.IsInstanceOf(typeof(Controller), carrinhoDeComprasController);
        }
    }
}