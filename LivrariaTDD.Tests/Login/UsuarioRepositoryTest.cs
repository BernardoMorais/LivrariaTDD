using System.Linq;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Login
{
    [TestFixture]
    public class UsuarioRepositoryTest
    {
        private IUsuarioRepository _repository;
        private EnumerableQuery<IUsuario> _listaDeUsuarios;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _listaDeUsuarios = new EnumerableQuery<IUsuario>(new[]
                {
                    new DAL.Models.Usuario 
                        {
                            Nome = "Cliente da Loja",
                            Email = "cliente@email.com",
                            Senha = "7c4a8d09ca3762af61e59520943dc26494f8941b", //123456
                            TipoUsuario = "cliente"
                        },
                    new DAL.Models.Usuario
                        {
                            Nome = "Funcionario da Loja",
                            Email = "funcionario@email.com",
                            Senha = "dd5fef9c1c1da1394d6d34b248c51be2ad740840", //654321
                            TipoUsuario = "funcionario"
                        }
                });
        }

        [Test]
        public void ACadamadaDeAcessoADadosDeveRecuperarUmUsuarioAtravesDoEmail_OUsuarioDeveSerRetornadoParaCamadaSuperior()
        {
            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Usuarios).Returns(_listaDeUsuarios);

            _repository = new UsuarioRepository(mockContext.Object);

            const string email = "funcionario@email.com";

            var result = _repository.RecuperarUsuario(email);

            Assert.IsInstanceOf<IUsuario>(result);
            StringAssert.AreEqualIgnoringCase(result.Email, email);
        }

        [Test]
        public void ACadamadaDeAcessoADadosDeveRecuperarUmUsuarioAtravesDoEmail_OUsuarioDeveVirDaBaseDeDados()
        {
            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Usuarios).Returns(_listaDeUsuarios);

            _repository = new UsuarioRepository(mockContext.Object);

            const string email = "funcionario@email.com";

            _repository.RecuperarUsuario(email);

            mockContext.Verify(x => x.Usuarios, Times.AtLeastOnce());
        }

        [Test]
        public void ACadamadaDeAcessoADadosDeveRecuperarUmUsuarioAtravesDoEmailQueNãoExista_ORetornoDeveSerNulo()
        {
            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Usuarios).Returns(_listaDeUsuarios);

            _repository = new UsuarioRepository(mockContext.Object);

            const string email = "não existe";

            var result = _repository.RecuperarUsuario(email);

            Assert.IsNull(result);
        }
    }
}