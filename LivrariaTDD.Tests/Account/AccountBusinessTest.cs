using LivrariaTDD.BRL.Account;
using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;
using Moq;

namespace LivrariaTDD.MVCTests.Account
{
    [TestFixture]
    public class AccountBusinessTest
    {
        private IAccountBusiness _business;
        private User _usuario;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _usuario = new User
            {
                Name = "Funcionario da Loja",
                Email = "funcionario@email.com",
                Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                UserType = "funcionario"
            };
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmailECompararComUsuarioDeMesmoEmailESenha_DeveRetornarVerdadeiro()
        {
            const string email = "funcionario@email.com";

            const string senha = "dd5fef9c1c1da1394d6d34b248c51be2ad740840";

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns(_usuario);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.CheckUser(email, senha);

            Assert.IsTrue(result);
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmailECompararComUsuarioDeMesmoEmailESenhaDiferente_DeveRetornarFalso()
        {
            const string email = "funcionario@email.com";

            const string senha = "senha errada";

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns(_usuario);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.CheckUser(email, senha);

            Assert.IsFalse(result);
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmailECompararComUsuarioNulo_DeveRetornarFalso()
        {
            const string email = "não existe";

            const string senha = "dd5fef9c1c1da1394d6d34b248c51be2ad740840";

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns((User) null);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.CheckUser(email, senha);

            Assert.IsFalse(result);
        }

        [Test]
        public void ACadamadaDeNegociosDeveRecuperarUmUsuarioAtravesDoEmail_DeveRetornarOTipoDoUsuario()
        {
            const string email = "funcionario@email.com";

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns(_usuario);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.VerificarTipoUsuario(email);

            StringAssert.AreEqualIgnoringCase(result, "funcionario");
        }

        [Test]
        public void ACadamadaDeNegociosDeveNaoRecuperarUmUsuario_DeveRetornarTipoVazio()
        {
            const string email = "funcionario@email.com";

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.RecuperarUsuario(email)).Returns((User) null);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.VerificarTipoUsuario(email);

            StringAssert.AreEqualIgnoringCase(result, "");
        }

        [Test]
        public void AoSolicitarRegistroDeNovoUsuario_ACamadaDeNegociosDeveAcessarACamadaDeAcessoADadosParaSalvarOUsuario()
        {
            var user = new User
            {
                Name = "",
                Email = "",
                Phone = "",
                CellPhone = "",
                Address = "",
                Number = 0,
                City = "",
                District = "",
                ZipCode = 0,
                State = "",
                Password = "",
                UserType = ""
            };

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.SaveUser(user)).Returns(true);

            _business = new AccountBusiness(mockContext.Object);

            _business.SaveUser(user);

            mockContext.Verify(x => x.SaveUser(user), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarRegistroDeNovoUsuarioValido_DeveRetornarVedadeiroCasoOUsuarioSejaSalvo()
        {
            var user = new User
            {
                Name = "Daniel Silva Moreira",
                Email = "daniel.smoreira@outlook.com",
                Phone = "3133333333",
                CellPhone = "3188888888",
                Address = "Rua Teste",
                Number = 123,
                City = "Belo Horizonte",
                District = "Centro",
                ZipCode = 30246130,
                State = "MG",
                Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                UserType = "user"
            };

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.SaveUser(user)).Returns(true);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.SaveUser(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void AoSolicitarRegistroDeNovoUsuarioInvalido_DeveRetornarFalsoCasoOUsuarioSejaSalvo()
        {
            var user = new User
            {
                Name = "",
                Email = "",
                Phone = "",
                CellPhone = "",
                Address = "",
                Number = 0,
                District = "",
                City = "",
                ZipCode = 0,
                State = "",
                Password = "",
                UserType = ""
            };

            var mockContext = new Mock<IAccountRepository>();

            mockContext.Setup(x => x.SaveUser(user)).Returns(false);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.SaveUser(user);

            Assert.IsFalse(result);
        }

        [Test]
        public void AoSolicitarChecagemDeEmail_OMetodoDeChecagemDaCamadaDeRepositorioDeveSerAcessada()
        {
            var mockContext = new Mock<IAccountRepository>();

            const string email = "email@email.com";

            mockContext.Setup(x => x.CheckEmail(email)).Returns(true);

            _business = new AccountBusiness(mockContext.Object);

            var result = _business.CheckEmail(email);   

            mockContext.Verify(x => x.CheckEmail(email), Times.AtLeastOnce());
        }
    }
}
