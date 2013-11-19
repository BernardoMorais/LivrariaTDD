using System.Collections;
using LivrariaTDD.Controllers.Livros;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Models;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;
using Omu.ValueInjecter;

namespace LivrariaTDD.MVCTests.ExcluirLivro
{
    [TestFixture]
    public class ExcluirLivroControllerTest
    {
        private LivroController _controller;
        private Mock<IProductBusiness> _business;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _business = new Mock<IProductBusiness>();
            _controller = new LivroController(_business.Object);
        }

        [Test]
        public void QuandoUsuarioSolicitarExclusaoDeUmLivro_OControleDeveManterOsDadosDoUsuario()
        {
            const int idLivro = 23;

            _controller = new LivroController(_business.Object);

            var result = _controller.ExcluirLivro(idLivro);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        }

        [Test]
        public void AposSolicitarExclusaoDeUmLivro_OControleDeveEncaminharOUsuarioParaPaginaDeListagemDeLivro()
        {
            const int idLivro = 23;

            _business.Setup(x => x.ExcluirLivro(idLivro)).Returns(true);

            _controller = new LivroController(_business.Object);

            var result = _controller.ExcluirLivro(idLivro);

            StringAssert.AreEqualIgnoringCase("ListagemDeProdutos", result.ViewName);
        }

        [Test]
        public void AposSolicitarExclusaoDeUmLivroEmCasoDeSucesso_OControleDeveRetornarUmaMensagemDeSucesso()
        {
            const int idLivro = 23;

            _business.Setup(x => x.ExcluirLivro(idLivro)).Returns(true);

            _controller = new LivroController(_business.Object);

            var result = _controller.ExcluirLivro(idLivro);

            CollectionAssert.Contains(result.ViewData.Keys, "Sucesso");

            StringAssert.AreEqualIgnoringCase(result.ViewData["Sucesso"] as string, "O livro foi excluído com sucesso.");
        }

        [Test]
        public void AposSolicitarExclusaoDeUmLivroEmCasoDeFalha_OControleDeveRetornarUmaMensagemDeFalha()
        {
            const int idLivro = 23;

            _business.Setup(x => x.ExcluirLivro(idLivro)).Returns(false);

            _controller = new LivroController(_business.Object);

            var result = _controller.ExcluirLivro(idLivro);

            CollectionAssert.Contains(result.ViewData.Keys, "Erro");

            StringAssert.AreEqualIgnoringCase(result.ViewData["Erro"] as string,"O livro não foi excluído com sucesso. Por gentileza tente novamente.");
        }
    }
}
