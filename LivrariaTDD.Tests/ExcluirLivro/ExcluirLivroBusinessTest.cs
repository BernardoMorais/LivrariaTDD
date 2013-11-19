using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.ExcluirLivro
{
    public class ExcluirLivroBusinessTest
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
        public void QuandoAlgumPersonagemSolicitaCadastroDeUmNovoLivro_ACamadaDeNegociosDeveAcessarACamadaDeAcessoADadosParaExcluirOLivro()
        {
            const int idLivro = 1;

            _repository.Setup(x => x.ExcluirLivro(idLivro)).Returns(true);

            _business.ExcluirLivro(idLivro);

            _repository.Verify(x => x.ExcluirLivro(idLivro), Times.AtLeastOnce());
        }
    }
}
