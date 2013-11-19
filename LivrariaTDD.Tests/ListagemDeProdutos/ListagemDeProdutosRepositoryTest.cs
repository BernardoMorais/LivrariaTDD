using System.Collections.Generic;
using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.ListagemDeProdutos
{
    [TestFixture]
    public class ListagemDeProdutosRepositoryTest
    {
        private IProductRepository _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach (var item in db.Products.ToList())
                {
                    db.Products.Remove(item);
                }

                db.SaveChanges();

                var produto = new Product
                {
                    Name = "TDD desenvolvimento guiado por testes",
                    Author = "Kent Beck",
                    Publishing = "Bookman",
                    Year = 2010,
                    Category = Categories.LiteraturaEstrangeira,
                    Stock = 0,
                    Price = 50.0M,
                    Photo = "",
                    Status = ProductStatus.Active
                };

                db.Products.Add(produto);
                db.SaveChanges();
            } 
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeListagemDaPaginaDeListagemEOcorreUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            var mockContext = new LivrariaTDDContext("server=./SQLServerStringErrada");

            //mockContext.Setup(x => x.Produtos).Throws<Exception>();

            _repository = new ProdutoRepository(mockContext);

            Assert.Throws<System.Data.DataException>(() => _repository.RecuperarTodosProdutos());
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirDaCamadaDeAcessoADados()
        {
            var mockContext = new LivrariaTDDContext();

            //mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext);

            var result = _repository.RecuperarTodosProdutos();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirSomenteLivrosComStatus1Ativo()
        {
            using (var livrariaContext = new LivrariaTDDContext())
            {
                var novoLivro = new Product
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
                        Name = "Torre Negra Volume 2",
                        Author = "Stephen King",
                        Publishing = "Universal",
                        Year = 1998,
                        Category = Categories.LiteraturaEstrangeira,
                        Stock = 8,
                        Price = 170.0M,
                        Photo = "",
                        Status = ProductStatus.Inative
                    };

                livrariaContext.Products.Add(novoLivro);
                livrariaContext.Products.Add(novoLivro2);
                livrariaContext.SaveChanges();
            }

            var mockContext = new LivrariaTDDContext();

            _repository = new ProdutoRepository(mockContext);

            var result = _repository.GetActiveProducts();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, mockContext.Products.Count());
            Assert.AreEqual(2, result.Count);
            Assert.IsInstanceOf<List<Product>>(result);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.IsNotEmpty(result);
        }
    }
}