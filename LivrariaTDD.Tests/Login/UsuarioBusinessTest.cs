using LivrariaTDD.BRL;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;
using Moq;

namespace LivrariaTDD.MVCTests.Login
{
    [TestFixture]
    public class UsuarioBusinessTest
    {
        private IUsuarioBusiness _business;
        private DAL.Models.Usuario _usuario;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _usuario = new DAL.Models.Usuario
            {
                Nome = "Funcionario da Loja",
                Email = "funcionario@email.com",
                Senha = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                TipoUsuario = "funcionario"
            };
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmailECompararComUsuarioDeMesmoEmailESenha_DeveRetornarVerdadeiro()
        {
            const string email = "funcionario@email.com";

            const string senha = "dd5fef9c1c1da1394d6d34b248c51be2ad740840";

            var mockContext = new Mock<IUsuarioRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns(_usuario);

            _business = new UsuarioBusiness(mockContext.Object);

            var result = _business.ValidarUsuario(email, senha);

            Assert.IsTrue(result);
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmailECompararComUsuarioDeMesmoEmailESenhaDiferente_DeveRetornarFalso()
        {
            const string email = "funcionario@email.com";

            const string senha = "senha errada";

            var mockContext = new Mock<IUsuarioRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns(_usuario);

            _business = new UsuarioBusiness(mockContext.Object);

            var result = _business.ValidarUsuario(email, senha);

            Assert.IsFalse(result);
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmailECompararComUsuarioNulo_DeveRetornarFalso()
        {
            const string email = "não existe";

            const string senha = "dd5fef9c1c1da1394d6d34b248c51be2ad740840";

            var mockContext = new Mock<IUsuarioRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns((IUsuario) null);

            _business = new UsuarioBusiness(mockContext.Object);

            var result = _business.ValidarUsuario(email, senha);

            Assert.IsFalse(result);
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmail_DeveRetornarOTipoDoUsuario()
        {
            const string email = "funcionario@email.com";

            var mockContext = new Mock<IUsuarioRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns(_usuario);

            _business = new UsuarioBusiness(mockContext.Object);

            var result = _business.VerificarTipoUsuario(email);

            StringAssert.AreEqualIgnoringCase(result, "funcionario");
        }

        [Test]
        public void ACadamadaDeNegociosDeveNaoRecuperarUmUsuario_DeveRetornarTipoVazio()
        {
            const string email = "funcionario@email.com";

            var mockContext = new Mock<IUsuarioRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns((IUsuario) null);

            _business = new UsuarioBusiness(mockContext.Object);

            var result = _business.VerificarTipoUsuario(email);

            StringAssert.AreEqualIgnoringCase(result, "");
        }
    }
}
