using System;
using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroBusinessTest
    {
        private IProductBusiness _business;
        private Mock<IProductRepository> _repository;
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

            _repository = new Mock<IProductRepository>();
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);
            _business = new ProductBusiness(_repository.Object);
        }
        
        #region US3

        [Test]
        public void AoAcessarACamadaDeNegociosParaVisualizarUmLivro_OLivroDeveVirDaCamadaDeAcessoADados()
        {
            const int id = 1;

            var result = _business.GetInfo(id);

            _repository.Verify(x => x.RecuperarInformacoesDoLivro(id), Times.AtLeastOnce());
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Product>(result);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeVisualizacaoEOcorrerUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            const int id = 1;

            var repository = new Mock<IProductRepository>();

            repository.Setup(x => x.RecuperarInformacoesDoLivro(id)).Throws<Exception>();

            var business = new ProductBusiness(repository.Object);

            Assert.Throws<Exception>(() => business.GetInfo(id));
        }

        [Test]
        public void AoAcessarACamadaDeNegociosParaAlterarUmLivro_OLivroDeveSerEnviadoParaSeSalvoNaCamadaDeAcessoADados()
        {
            var novosValores = new Product
            {
                ProductId = 3,
                Name = "A Bela e a Fera",
                Author = "Popular",
                Publishing = "Abril",
                Year = 2005,
                Category = Categories.InfantoJuvenis,
                Stock = 10,
                Price = 10.0M,
                Photo = ""
            };

            _repository.Setup(x => x.AlterarLivro(novosValores)).Returns(true);

            var result = _business.Update(novosValores);

            _repository.Verify(x => x.AlterarLivro(novosValores), Times.AtLeastOnce());
            Assert.True(result);
        }

        #endregion
    }
}