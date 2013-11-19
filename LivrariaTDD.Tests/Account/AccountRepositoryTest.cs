using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Account
{
    [TestFixture]
    public class AccountRepositoryTest
    {
        private IAccountRepository _repository;

        [SetUp]
        public void SetUp()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach (var item in db.Orders.ToList())
                {
                    db.Orders.Remove(item);
                }

                foreach (var item in db.Users.ToList())
                {
                    db.Users.Remove(item);
                }

                foreach (var item in db.Products.ToList())
                {
                    db.Products.Remove(item);
                }

                foreach (var item in db.PaymentTypes.ToList())
                {
                    db.PaymentTypes.Remove(item);
                }
                var cliente = new User
                    {
                        Name = "Cliente da Loja",
                        Email = "cliente@email.com",
                        Phone = "3133333333",
                        CellPhone = "3188888888",
                        Address = "Rua Teste",
                        Number = 123,
                        District = "Centro",
                        City = "Belo Horizonte",
                        ZipCode = 30246130,
                        State = "MG",
                        Password = "7c4a8d09ca3762af61e59520943dc26494f8941b", //123456
                        UserType = "user"
                    };
                var funcionario = new User
                    {
                        Name = "Funcionario da Loja",
                        Email = "funcionario@email.com",
                        Phone = "3133333333",
                        CellPhone = "3188888888",
                        Address = "Rua Teste",
                        District = "Centro",
                        Number = 123,
                        City = "Belo Horizonte",
                        ZipCode = 30246130,
                        State = "MG",
                        Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840", //654321
                        UserType = "admin"
                    };

                db.Users.Add(cliente);
                db.Users.Add(funcionario);
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach (var item in db.Orders.ToList())
                {
                    db.Orders.Remove(item);
                }

                foreach (var item in db.Users.ToList())
                {
                    db.Users.Remove(item);
                }

                foreach (var item in db.Products.ToList())
                {
                    db.Products.Remove(item);
                }

                foreach (var item in db.PaymentTypes.ToList())
                {
                    db.PaymentTypes.Remove(item);
                }
                db.SaveChanges();
            }
        }


        [Test]
        public void ACadamadaDeAcessoADadosDeveRecuperarUmUsuarioAtravesDoEmail_OUsuarioDeveSerRetornadoParaCamadaSuperior()
        {
            var mockContext = new LivrariaTDDContext();

            _repository = new AccountRepository(mockContext);

            const string email = "funcionario@email.com";

            var result = _repository.RecuperarUsuario(email);

            Assert.IsInstanceOf<User>(result);
            StringAssert.AreEqualIgnoringCase(result.Email, email);
        }

        [Test]
        public void ACadamadaDeAcessoADadosDeveRecuperarUmUsuarioAtravesDoEmailQueNãoExista_ORetornoDeveSerNulo()
        {
            var mockContext = new LivrariaTDDContext();

            //mockContext.Setup(x => x.Usuarios).Returns(_listaDeUsuarios);

            _repository = new AccountRepository(mockContext);

            const string email = "não existe";

            var result = _repository.RecuperarUsuario(email);

            Assert.IsNull(result);
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmUsuarioValido_DeveSalvarOUsuarioERetornarVerdadeiro()
        {
            var user = new User
            {
                Name = "Daniel Silva Moreira",
                Email = "daniel.smoreira@outlook.com",
                Phone = "3133333333",
                CellPhone = "3188888888",
                Address = "Rua Teste",
                Number = 123,
                District = "Centro",
                City = "Belo Horizonte",
                ZipCode = 30246130,
                State = "MG",
                Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                UserType = "user"
            };

            var auxContext = new LivrariaTDDContext();

            var mockContext = new LivrariaTDDContext();

            _repository = new AccountRepository(mockContext);

            var countAntes = auxContext.Users.Count();

            var result = _repository.SaveUser(user);

            var countDepois = auxContext.Users.Count();

            Assert.True(result);
            Assert.AreEqual(2, countAntes);
            Assert.AreEqual(3, countDepois);
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmUsuarioEReceberUmErro_NaoDeveSalvarOUsuarioERetonarFalso()
        {
            var user = new User
            {
                Name = "Daniel Silva Moreira",
                Email = "daniel.smoreira@outlook.com",
                Phone = "3133333333",
                CellPhone = "3188888888",
                Address = "Rua Teste",
                Number = 123,
                District = "Centro",
                City = "Belo Horizonte",
                ZipCode = 30246130,
                State = "MG",
                Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                UserType = "user"
            };

            var auxContext = new LivrariaTDDContext();

            var mockContext = new LivrariaTDDContext("server=./SQLSeverS/tring Errada");

            _repository = new AccountRepository(mockContext);

            var countAntes = auxContext.Users.Count();

            var result = _repository.SaveUser(user);

            var countDepois = auxContext.Users.Count();

            Assert.False(result);
            Assert.AreEqual(2, countAntes);
            Assert.AreEqual(2, countDepois);
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmUsuarioInvalido_NaoDeveSalvarOUsuarioERetonarFalso()
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

            var auxContext = new LivrariaTDDContext();

            var mockContext = new LivrariaTDDContext();

            _repository = new AccountRepository(mockContext);

            var countAntes = auxContext.Users.Count();

            var result = _repository.SaveUser(user);

            var countDepois = auxContext.Users.Count();

            Assert.False(result);
            Assert.AreEqual(2, countAntes);
            Assert.AreEqual(2, countDepois);
        }

        [Test]
        public void AoAcessarACadaDeAcessoADadosParaConsultarSeDeterminadoEmailJaEstaCadastrado_DeveRetornarVerdadeiroCasoExitaOEmail()
        {
            var user = new User
            {
                Name = "Daniel Silva Moreira",
                Email = "daniel.smoreira@outlook.com",
                Phone = "3133333333",
                CellPhone = "3188888888",
                Address = "Rua Teste",
                Number = 123,
                District = "Centro",
                City = "Belo Horizonte",
                ZipCode = 30246130,
                State = "MG",
                Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                UserType = "user"
            };

            const string email = "daniel.smoreira@outlook.com";

            var auxContext = new LivrariaTDDContext();

            auxContext.Users.Add(user);

            auxContext.SaveChanges();

            var mockContext = new LivrariaTDDContext();

            _repository = new AccountRepository(mockContext);

            var result = _repository.CheckEmail(email);

            Assert.True(result);
        }

        [Test]
        public void AoAcessarACadaDeAcessoADadosParaConsultarSeDeterminadoEmailJaEstaCadastrado_DeveRetornarFalsoCasoNaoExitaOEmail()
        {
            var user = new User
            {
                Name = "Daniel Silva Moreira",
                Email = "daniel.smoreira@outlook.com",
                Phone = "3133333333",
                CellPhone = "3188888888",
                Address = "Rua Teste",
                Number = 123,
                District = "Centro",
                City = "Belo Horizonte",
                ZipCode = 30246130,
                State = "MG",
                Password = "dd5fef9c1c1da1394d6d34b248c51be2ad740840",
                UserType = "user"
            };

            const string email = "emailInexistente@outlook.com";

            var auxContext = new LivrariaTDDContext();

            auxContext.Users.Add(user);

            auxContext.SaveChanges();

            var mockContext = new LivrariaTDDContext();

            _repository = new AccountRepository(mockContext);

            var result = _repository.CheckEmail(email);

            Assert.False(result);
        }
    }
}