using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroRepositoryTest
    {
        private IProductRepository _repository;
        private Product _livroTDD;
        private Product _livroRomance;
        private Product _livroFiccao;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _livroTDD = new Product
            {
                ProductId = 1,
                Name = "TDD desenvolvimento guiado por testes",
                Author = "Kent Beck",
                Publishing = "Bookman",
                Year = 2010,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 0,
                Price = 50.0M,
                Photo = ""
            };

            _livroRomance = new Product
            {
                ProductId = 2,
                Name = "O Amor",
                Author = "Escritora Romance",
                Publishing = "Bookman",
                Year = 2007,
                Category = Categories.LiteraturaBrasileira,
                Stock = 0,
                Price = 30.0M,
                Photo = ""
            };

            _livroFiccao = new Product
            {
                ProductId = 3,
                Name = "O Senhor Dos Aneis",
                Author = "Tolken J.R.",
                Publishing = "Abril",
                Year = 2005,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 0,
                Price = 100.0M,
                Photo = ""
            };

            using (var db = new LivrariaTDDContext())
            {
                _livroTDD = db.Products.Add(_livroTDD);
                _livroRomance = db.Products.Add(_livroRomance);
                _livroFiccao = db.Products.Add(_livroFiccao);
                db.SaveChanges();
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach(var item in db.Products.ToList())
                {
                    db.Products.Remove(item);
                }
                db.SaveChanges();
            }
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeVisualizacaoDeProdutoEOcorrerUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            const int id = 1;

            var mockContext = new LivrariaTDDContext("server=./SQLServerStringErrada");

            //mockContext.Setup(x => x.Produtos).Throws<Exception>();

            _repository = new ProdutoRepository(mockContext);

            Assert.Throws<System.Data.DataException>(() => _repository.RecuperarInformacoesDoLivro(id));
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeVisualizacaoDeLivro_OProdutoDeveSerRetornado()
        {
            var mockContext = new LivrariaTDDContext();

            //mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext);

            var result = _repository.RecuperarInformacoesDoLivro(_livroFiccao.ProductId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Product>(result);
            Assert.AreEqual(result.ProductId, _livroFiccao.ProductId);
        }

        [Test]
        public void AoAcessarACamdadaDeAcessoADadosParaAlterarUmProduto_OProdutoDeveSerSalvoEUmResultadoBooleYearDeveSerRetornado()
        {
            var novosValores = new Product
            {
                ProductId = 3,
                Name = "Cinderela",
                Author = "Popular",
                Publishing = "Abril",
                Year = 2005,
                Category = Categories.InfantoJuvenis,
                Stock = 10,
                Price = 10.0M,
                Photo = ""
            };

            var mockContext = new LivrariaTDDContext();

            novosValores = mockContext.Products.Add(novosValores);
            mockContext.SaveChanges();

            //mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext);

            var aux = _repository.RecuperarInformacoesDoLivro(novosValores.ProductId);

            var livroAntigo = new Product
                {
                    ProductId = aux.ProductId,
                    Name = aux.Name,
                    Author = aux.Author,
                    Publishing = aux.Publishing,
                    Year = aux.Year,
                    Category = aux.Category,
                    Stock = aux.Stock,
                    Price = aux.Price,
                    Photo = aux.Photo
                };

            novosValores.Name = "Name novo";

            var result = _repository.AlterarLivro(novosValores);

            var livroNovo = _repository.RecuperarInformacoesDoLivro(novosValores.ProductId);

            Assert.True(result);
            Assert.AreEqual(livroAntigo.ProductId, livroNovo.ProductId);
            StringAssert.AreNotEqualIgnoringCase(livroAntigo.Name, livroNovo.Name);
        }
    }
}