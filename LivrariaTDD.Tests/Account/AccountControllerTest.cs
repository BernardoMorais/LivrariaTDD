using System.Diagnostics;
using System.Web.Mvc;
using LivrariaTDD.Controllers.Account;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Login
{
    [TestFixture]
    public class AccountControllerTest
    {
        private const string SenhaCriptografada = "dd5fef9c1c1da1394d6d34b248c51be2ad740840";
        private const string SenhaNaoCriptografada = "654321";
        private AccountController _controller;

        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [Test]
        public void AoControleEnviarOsDadosDoUsuario_ComoFuncionarioDaLoja_ASenhaDeveSerCriptografadaComOProtocoloSHA1()
        {
            StringAssert.AreEqualIgnoringCase(SenhaCriptografada, Helpers.ConvertToSHA1(SenhaNaoCriptografada));
        }

        //[Test]
        //public void AoControleEnviarOsDadosCorretosDoUsuario_ComoFuncionarioDaLoja_OControleDeveAutorizarOCookieDoUsuario()
        //{
        //    var usuario = new Models.Account.Login {Email = "admin@email.com", Password = "654321"};

        //    var business = new Mock<IAccountBusiness>();
        //    business.Setup(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password))).Returns(true);

        //    _controller = new AccountController(business.Object);

        //    var result = _controller.Login(usuario) as RedirectToRouteResult;

        //    Assert.True((bool)result.RouteValues["Logado"]);
        //}

        //[Test]
        //public void AoControleEnviarOsDadosIncorretosDoUsuario_ComoFuncionarioDaLoja_OControleNaoDeveAutorizarOCookieDoUsuario()
        //{
        //    var usuario = new Models.Account.Login { Email = "funcionario@email.com", Password = "senhaErrada" };

        //    var business = new Mock<IAccountBusiness>();
        //    business.Setup(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password))).Returns(false);

        //    _controller = new AccountController(business.Object);

        //    var result = _controller.Login(usuario) as RedirectToRouteResult;

        //    Assert.False((bool)result.RouteValues["Logado"]);
        //}

        //[Test]
        //public void AoSolicitarLogoff_ComoFuncionarioDaLoja_OControleDeveRemoverOCookieDeAutorizacaoENaoDeveAutorizarOCookieDoUsuario()
        //{
        //    var usuario = new Models.Account.Login { Email = "funcionario@email.com", Password = "654321" };

        //    var business = new Mock<IAccountBusiness>();
        //    business.Setup(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password))).Returns(true);

        //    _controller = new AccountController(business.Object);

        //    _controller.Login(usuario);

        //    var result = _controller.Logout() as RedirectToRouteResult;

        //    Assert.False((bool)result.RouteValues["logado"]);
        //}

        //[Test]
        //public void AoControleEnviarOsDadosCorretosDoUsuario_ComoFuncionarioDaLoja_OControleDeveRetornarParaPaginaDeListagemDeProdutos()
        //{
        //    var usuario = new Models.Account.Login { Email = "funcionario@email.com", Password = "654321" };

        //    var business = new Mock<IAccountBusiness>();
        //    business.Setup(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password))).Returns(true);

        //    _controller = new AccountController(business.Object);

        //    var result = _controller.Login(usuario) as RedirectToRouteResult;

        //    StringAssert.AreEqualIgnoringCase("Home", result.RouteValues["controller"] as string);
        //    StringAssert.AreEqualIgnoringCase("Index", result.RouteValues["action"] as string);
        //}

        [Test]
        public void AoControleEnviarOsDadosIncorretosDoUsuario_ComoFuncionarioDaLoja_OControleDeveRetornarParaPaginaDeLoginEInformarQueOsDadosEstaoIncorretos()
        {
            var usuario = new Models.Account.Login { Email = "funcionario@email.com", Password = "senhaErrada" };

            var business = new Mock<IAccountBusiness>();
            business.Setup(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password))).Returns(false);

            _controller = new AccountController(business.Object);

            var result = _controller.Login(usuario) as ViewResult;

            StringAssert.AreEqualIgnoringCase("Login", result.ViewName);
            StringAssert.AreEqualIgnoringCase("Dados do usuário inválidos.", result.ViewData["erroLogin"] as string);
        }

        //[Test]
        //public void AoSolicitarLoginAoControle_ComoFuncionarioDaLoja_OControleDeveVerificarAValidadeDoUsuarioNaCamadaDeNegocios()
        //{
        //    var usuario = new Models.Account.Login { Email = "funcionario@email.com", Password = "654321" };

        //    var business = new Mock<IAccountBusiness>();
        //    business.Setup(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password))).Returns(true);

        //    _controller = new AccountController(business.Object);

        //    _controller.Login(usuario);

        //    business.Verify(x => x.CheckUser(usuario.Email, Helpers.ConvertToSHA1(usuario.Password)), Times.AtLeastOnce());
        //}

        [Test]
        public void AoAcessarATelaDeLogin_OControlerDeveEnviarUmObjetoModelDoTipoLoginParaTela()
        {
            var business = new Mock<IAccountBusiness>();

            var accountController = new AccountController(business.Object);

            var result = accountController.Login() as ViewResult;

            var type = result.Model.GetType();

            var test = type == typeof(Models.Account.Login);

            Assert.IsTrue(test);
        }
    }
}
