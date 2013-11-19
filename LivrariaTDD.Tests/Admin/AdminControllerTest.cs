using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using LivrariaTDD.Controllers.Admin;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace LivrariaTDD.MVCTests.Admin
{
    [TestFixture]
    public class AdminControllerTest
    {
        private IAccountBusiness _serviceMock;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _serviceMock = new Mock<IAccountBusiness>().Object;
        }

        [Test]
        public void AoAcessarAPaginaDeLoginComoAdministradores_OUsuarioDeveSerDirecionadoParaPaginaLoginDeAdministradores()
        {
            var identity = new Mock<IIdentity>();
            identity.SetupGet(x => x.IsAuthenticated).Returns(false); //Not logged

            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity).Returns(identity.Object);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(principal.Object);

            var adminController = new AdminController(_serviceMock);
            adminController.ControllerContext = new ControllerContext(context.Object, new RouteData(), adminController);

            var result = adminController.Login() as ViewResult;
            Assert.IsNotNull(result);
            StringAssert.AreEqualIgnoringCase("Login", result.ViewName);
        }

        [Test]
        public void AoAcessarAPaginaDeLoginDeAdministradores_ComoAdministrador_OUsuarioDeveSerDirecionadoParaPaginaInicialDeAdministradores()
        {
            var identity = new Mock<IIdentity>();
            identity.SetupGet(x => x.IsAuthenticated).Returns(true); //Logged

            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity).Returns(identity.Object);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(principal.Object);

            var adminController = new AdminController(_serviceMock);
            adminController.ControllerContext = new ControllerContext(context.Object, new RouteData(), adminController);

            var result = adminController.Login() as RedirectToRouteResult;
            Assert.IsNotNull(result);
            CollectionAssert.Contains(result.RouteValues.Values, "Index");
        }

        [Test]
        public void AoAccesarAPaginaPrincipalDeAdministradores_OSistemaDevePossuirUmAttributoDeAutorizacao()
        {
            var controller = new AdminController(_serviceMock);
            var type = controller.GetType();
            var methodInfo = type.GetMethod("Index");
            var attributes = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            Assert.IsTrue(attributes.Any(), "No AuthorizeAttribute found");
        }
    
        [Test]
        public void AoAcessarAPaginaPrincipalDeAdministraroes_OSistemaDeveDirecionarParaPaginaPrincipal()
        {
            var productBusinessMock = new Mock<IProductBusiness>();

            productBusinessMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            var adminController = new AdminController(_serviceMock, productBusinessMock.Object);
            var result = adminController.Index() as ViewResult;
            Assert.IsNotNull(result);
            StringAssert.AreEqualIgnoringCase("Index", result.ViewName);
        }
    }
}