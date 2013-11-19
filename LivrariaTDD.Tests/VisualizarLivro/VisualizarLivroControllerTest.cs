using System.Collections;
using LivrariaTDD.Controllers.Livros;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroControllerTest
    {
        private LivroController _controller;
        private Mock<IProductBusiness> _business;
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

            _business = new Mock<IProductBusiness>();
            _business.Setup(x => x.GetInfo(1)).Returns(_livroTDD);
            _business.Setup(x => x.GetInfo(2)).Returns(_livroRomance);
            _business.Setup(x => x.GetInfo(3)).Returns(_livroFiccao);
            _controller = new LivroController(_business.Object);
        }

        #region US3

        [Test]
        public void QuandoUsuarioFiltarAListaPeloName_OControleDeveManterOsDadosDoUsuario()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetInfo(1)).Returns(_livroTDD);
            business.Setup(x => x.GetInfo(2)).Returns(_livroRomance);
            business.Setup(x => x.GetInfo(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            var result = _controller.VisualizarLivro(1);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        }

        [Test]
        public void AoSolicitarAVisualizacaoDeUmLivro_OSistemaDeveAbrirAPaginaDeVisualizacaoDeLivro()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetInfo(1)).Returns(_livroTDD);
            business.Setup(x => x.GetInfo(2)).Returns(_livroRomance);
            business.Setup(x => x.GetInfo(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            var result = _controller.VisualizarLivro(1);

            Assert.AreEqual(result.ViewName, "VisualizarLivro");
        }

        [Test]
        public void AoSolicitarAVisualizacaoDeUmLivro_OSistemaDeveBuscarAsInformacoesDoLivroNaCamadaDeNegocios()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetInfo(1)).Returns(_livroTDD);
            business.Setup(x => x.GetInfo(2)).Returns(_livroRomance);
            business.Setup(x => x.GetInfo(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            _controller.VisualizarLivro(1);

            business.Verify(x => x.GetInfo(1), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarAVisualizacaoDeUmLivro_OSistemaDevePassarAsInformacoesDoLivroParaTela()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetInfo(1)).Returns(_livroTDD);
            business.Setup(x => x.GetInfo(2)).Returns(_livroRomance);
            business.Setup(x => x.GetInfo(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            var result = _controller.VisualizarLivro(1);

            Assert.Contains("livro", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<Models.Product.Product>(result.ViewData["livro"]);
        }

        [Test]
        public void AoSolicitarAlteracaoDeUmLivro_OSistemaDeveEnviarAsInformacoesParaQueACamadaDeNegociosSalve()
        {
            var novosValores = new Models.Product.Product
            {
                ProductId = 3,
                Name = "A Bela e a Fera",
                Author = "Popular",
                Publishing = "Abril",
                Year = 2005,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 10,
                Price = 10.0M,
                Photo = ""
            };

            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.Update(It.IsAny<Product>())).Returns(true);

            var controller = new LivroController(business.Object);

            var result = controller.AlterarLivro(novosValores);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            Assert.Contains("livro", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<Models.Product.Product>(result.ViewData["livro"]);
            Assert.AreEqual(result.ViewName, "VisualizarLivro");

            Assert.Contains("Sucesso", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase(result.ViewData["Sucesso"] as string, "As informações foram gravadas com sucesso.");
        }

        [Test]
        public void AoSolicitarAlteracaoDeUmLivro_OSistemaDeveSalvarECarregarNovamenteComAsMesmasInformacoes()
        {
            var novosValores = new Models.Product.Product
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

            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.Update(It.IsAny<Product>())).Returns(true);

            var controller = new LivroController(business.Object);

            controller.AlterarLivro(novosValores);

            business.Verify(x => x.Update(It.IsAny<Product>()), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarAlteracaoDeUmLivro_OSistemaDeveInformarCasoOcorraAlgumErro()
        {
            var novosValores = new Models.Product.Product
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

            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.Update(It.IsAny<Product>())).Returns(false);

            var controller = new LivroController(business.Object);

            var result = controller.AlterarLivro(novosValores);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            Assert.Contains("livro", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<Models.Product.Product>(result.ViewData["livro"]);

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase(result.ViewData["Erro"] as string, "Ocorreu um erro durante o processamento e não foi possível salvar as alterações.");
        }

        #endregion
    }
}