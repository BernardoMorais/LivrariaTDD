using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.CadastrarLivro
{
    public class CadastrarLivroBusinessTest
    {
        private ProductBusiness _business;
        private Mock<IProductRepository> _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Mock<IProductRepository>();
            _business = new ProductBusiness(_repository.Object);
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitaCadastroDeUmNovoLivro_ACamadaDeNegociosDeveAcessarACamadaDeAcessoADadosParaSalvarOLivro()
        {
            var novoLivro = new Product
            {
                ProductId = 1,
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = ""
            };
            

            _repository.Setup(x => x.SalvarLivro(novoLivro)).Returns(novoLivro);

            _business.SalvarLivro(novoLivro);

            _repository.Verify(x => x.SalvarLivro(novoLivro),Times.AtLeastOnce());
        }

        [Test]
        public void QuandoACamadaDeNegociosSalvaUmLivroComSucesso_DeveRetornarVedadeiroParaQuemAChamou()
        {
            var novoLivro = new Product
            {
                ProductId = 1,
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = ""
            };

            _repository.Setup(x => x.SalvarLivro(novoLivro)).Returns(novoLivro);

            var result =_business.SalvarLivro(novoLivro);

            Assert.NotNull(result);
        }

        [Test]
        public void QuandoACamadaDeNegocioNaoSalvaUmLivroPorAlgumaFalha_DeveRetornarFalsoParaQuemAChamou()
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
                Photo = ""
            };

            _repository.Setup(x => x.SalvarLivro(novoLivro)).Returns((Product)null);

            var result = _business.SalvarLivro(novoLivro);

            Assert.Null(result);
        }
    }
}
