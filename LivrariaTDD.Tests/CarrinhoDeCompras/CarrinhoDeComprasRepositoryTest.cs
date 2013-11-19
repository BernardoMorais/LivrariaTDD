using System.Collections.Generic;
using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using NUnit.Framework;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.MVCTests.CarrinhoDeCompras
{
    [TestFixture]
    public class CarrinhoDeComprasRepositoryTest
    {
        private IOrderRepository _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach (var item in db.Orders)
                {
                    db.Orders.Remove(item);
                }

                foreach (var item in db.Products)
                {
                    db.Products.Remove(item);
                }

                foreach (var item in db.Users)
                {
                    db.Users.Remove(item);
                }

                foreach (var item in db.PaymentTypes)
                {
                    db.PaymentTypes.Remove(item);
                }

                db.SaveChanges();
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach (var item in db.Orders)
                {
                    db.Orders.Remove(item);
                }

                foreach (var item in db.Products)
                {
                    db.Products.Remove(item);
                }

                foreach (var item in db.Users)
                {
                    db.Users.Remove(item);
                }

                foreach (var item in db.PaymentTypes)
                {
                    db.PaymentTypes.Remove(item);
                }

                db.SaveChanges();
            }
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmaCompra_ACompraDeSerSalvaEOMetodoDeveRetornarVerdadeiro()
        {
            var novoLivro1 = new Product
            {
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = "",
                Status = ProductStatus.Active
            };

            var novoLivro2 = new Product
            {
                Name = "Torre Negra Segunda edição",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1998,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 8,
                Price = 170.0M,
                Photo = "",
                Status = ProductStatus.Active
            };

            var formaPagamento = new PaymentType
                {
                    PaymentTypeName = "Boleto Bancário",
                    Icon = ""
                };

            var usuario = new User
                {
                    District = "Centro",
                    CellPhone = "3188888888",
                    ZipCode = 30625130,
                    City = "Belo Horizonte",
                    State = "MG",
                    Name = "Jô Soares",
                    Email = "email@email.com.br",
                    Number = 456,
                    Address = "Afonso Pena",
                    Password = "123496469",
                    Phone = "313333333",
                    UserType = "Cliente"
                };

            using (var livrariaContext = new LivrariaTDDContext())
            {
                novoLivro1 = livrariaContext.Products.Add(novoLivro1);
                novoLivro2 = livrariaContext.Products.Add(novoLivro2);
                formaPagamento = livrariaContext.PaymentTypes.Add(formaPagamento);
                usuario = livrariaContext.Users.Add(usuario);
                livrariaContext.SaveChanges();
            }

            
            var pedido = new Order
                {
                    User = usuario,
                    PaymentType = formaPagamento,
                    Products = new List<Product> {novoLivro1, novoLivro2},
                    OrderValue = novoLivro1.Price + novoLivro2.Price,
                    FreightValue = 10.00M,
                    TotalValue = novoLivro1.Price + novoLivro2.Price + 10.00M
                };

            int userId = usuario.UserId;
            var listaIdProducts = new List<int>{ novoLivro1.ProductId, novoLivro2.ProductId };
            int formaPagamentoId = formaPagamento.PaymentTypeId;


            using (var auxContext = new LivrariaTDDContext())
            {
                var countAntes = auxContext.Orders.Count();
                Assert.AreEqual(0, countAntes);
            }

            using (var livrariaContext = new LivrariaTDDContext())
            {
                _repository = new PedidoRepository(livrariaContext);

                var result = _repository.SalvarPedido(pedido, userId, listaIdProducts, formaPagamentoId);

                Assert.True(result);
            }

            using (var auxContext = new LivrariaTDDContext())
            {
                var countDepois = auxContext.Orders.Count();
                Assert.AreEqual(1, countDepois);
            }
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaExcluirUmLivroNaoExistente_OMetodoDeveRetornarFalso()
        {
            var usuario = new User
            {
                District = "Centro",
                CellPhone = "3188888888",
                ZipCode = 30625130,
                City = "Belo Horizonte",
                State = "MG",
                Name = "Jô Soares",
                Email = "email@email.com.br",
                Number = 456,
                Address = "Afonso Pena",
                Password = "123496469",
                Phone = "313333333",
                UserType = "Cliente"
            };

            using (var livrariaContext = new LivrariaTDDContext())
            {
                usuario = livrariaContext.Users.Add(usuario);
                livrariaContext.SaveChanges();
            }

            var pedido = new Order
            {
                User = usuario,
                PaymentType = null,
                Products = new List<Product> { null, null },
                OrderValue = 0M,
                FreightValue = 10.00M,
                TotalValue = 0M + 10.00M
            };

            const int userId = -34;
            var listaIdProducts = new List<int> { -15, -10 };
            const int formaPagamentoId = -5;


            using (var auxContext = new LivrariaTDDContext())
            {
                var countAntes = auxContext.Orders.Count();
                Assert.AreEqual(0, countAntes);
            }

            using (var livrariaContext = new LivrariaTDDContext())
            {
                _repository = new PedidoRepository(livrariaContext);

                var result = _repository.SalvarPedido(pedido, userId, listaIdProducts, formaPagamentoId);

                Assert.False(result);
            }

            using (var auxContext = new LivrariaTDDContext())
            {
                var countDepois = auxContext.Orders.Count();
                Assert.AreEqual(0, countDepois);
            }
        }
    }
}
