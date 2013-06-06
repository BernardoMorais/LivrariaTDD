using LivrariaTDD.Controllers.Usuario;
using LivrariaTDD.Infrastructure.BRL.Usuario;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Login
{
    [TestFixture]
    public class UsuarioControllerTest
    {
        private const string SenhaCriptografada = "dd5fef9c1c1da1394d6d34b248c51be2ad740840";
        private const string SenhaNaoCriptografada = "654321";
        private UsuarioController _controller;

        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [Test]
        public void AoControleEnviarOsDadosDoUsuario_ComoFuncionarioDaLoja_ASenhaDeveSerCriptografadaComOProtocoloSHA1()
        {
            StringAssert.AreEqualIgnoringCase(SenhaCriptografada, Helpers.ConvertToSHA1(SenhaNaoCriptografada));
        }

        [Test]
        public void AoControleEnviarOsDadosCorretosDoUsuario_ComoFuncionarioDaLoja_OControleDeveAutorizarOCookieDoUsuario()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "654321" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);

            _controller = new UsuarioController(business.Object);

            var result = _controller.Entrar(usuario);

            Assert.True((bool)result.RouteValues["Logado"]);
        }

        [Test]
        public void AoControleEnviarOsDadosIncorretosDoUsuario_ComoFuncionarioDaLoja_OControleNaoDeveAutorizarOCookieDoUsuario()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "senhaErrada" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(false);

            _controller = new UsuarioController(business.Object);

            var result = _controller.Entrar(usuario);

            Assert.False((bool)result.RouteValues["Logado"]);
        }

        [Test]
        public void AoSolicitarLogoff_ComoFuncionarioDaLoja_OControleDeveRemoverOCookieDeAutorizacaoENaoDeveAutorizarOCookieDoUsuario()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "654321" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);

            _controller = new UsuarioController(business.Object);

            _controller.Entrar(usuario);

            var result = _controller.Sair();

            Assert.False((bool)result.RouteValues["logado"]);
        }

        [Test]
        public void AoControleEnviarOsDadosCorretosDoUsuario_ComoFuncionarioDaLoja_OControleDeveRetornarParaPaginaDeListagemDeProdutos()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "654321" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);
            business.Setup(x => x.VerificarTipoUsuario(usuario.Email)).Returns("Funcionario");

            _controller = new UsuarioController(business.Object);

            var result = _controller.Entrar(usuario);

            StringAssert.AreEqualIgnoringCase("ListagemDeProdutos", result.RouteValues["controller"] as string);
            StringAssert.AreEqualIgnoringCase("Index", result.RouteValues["action"] as string);
            StringAssert.AreEqualIgnoringCase("", result.RouteValues["erroLogin"] as string);
            StringAssert.AreEqualIgnoringCase("Funcionario", result.RouteValues["tipoUsuario"] as string);
        }

        [Test]
        public void AoControleEnviarOsDadosIncorretosDoUsuario_ComoFuncionarioDaLoja_OControleDeveRetornarParaPaginaDeLoginEInformarQueOsDadosEstaoIncorretos()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "senhaErrada" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(false);

            _controller = new UsuarioController(business.Object);

            var result = _controller.Entrar(usuario);

            StringAssert.AreEqualIgnoringCase("ListagemDeProdutos", result.RouteValues["controller"] as string);
            StringAssert.AreEqualIgnoringCase("Index", result.RouteValues["action"] as string);
            StringAssert.AreEqualIgnoringCase("Dados do usuário inválidos.", result.RouteValues["erroLogin"] as string);
            StringAssert.AreEqualIgnoringCase("", result.RouteValues["tipoUsuario"] as string);
        }

        [Test]
        public void AoSolicitarLoginAoControle_ComoFuncionarioDaLoja_OControleDeveVerificarAValidadeDoUsuarioNaCamadaDeNegocios()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "654321" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);

            _controller = new UsuarioController(business.Object);

            _controller.Entrar(usuario);

            business.Verify(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha)), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarLoginAoControle_OControleDeveVerficarOTipoDeUsuarioNaCamadaDeNegocios()
        {
            var usuario = new UsuarioModel { Email = "cliente@email.com", Senha = "123456" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);
            business.Setup(x => x.VerificarTipoUsuario(usuario.Email)).Returns("Cliente");

            _controller = new UsuarioController(business.Object);

            _controller.Entrar(usuario);

            business.Verify(x => x.VerificarTipoUsuario(usuario.Email), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarLoginAoControle_ComoFuncionarioDaLoja_OControleDeveRetornarOTipoCorrepondente()
        {
            var usuario = new UsuarioModel { Email = "funcionario@email.com", Senha = "654321" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);
            business.Setup(x => x.VerificarTipoUsuario(usuario.Email)).Returns("Funcionario");

            _controller = new UsuarioController(business.Object);

            var result = _controller.Entrar(usuario);

            StringAssert.AreEqualIgnoringCase("Funcionario", result.RouteValues["tipoUsuario"] as string);
        }

        [Test]
        public void AoSolicitarLoginAoControle_ComoFuncionarioCliente_OControleDeveRetornarOTipoCorrepondente()
        {
            var usuario = new UsuarioModel { Email = "cliente@email.com", Senha = "123456" };

            var business = new Mock<IUsuarioBusiness>();
            business.Setup(x => x.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha))).Returns(true);
            business.Setup(x => x.VerificarTipoUsuario(usuario.Email)).Returns("Cliente");

            _controller = new UsuarioController(business.Object);

            var result = _controller.Entrar(usuario);

            StringAssert.AreEqualIgnoringCase("Cliente", result.RouteValues["tipoUsuario"] as string);
        }
    }
}
