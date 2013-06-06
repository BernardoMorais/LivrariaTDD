using System.Collections;
using LivrariaTDD.Controllers.Livros;
using LivrariaTDD.Infrastructure.BRL.Livro;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroControllerTest
    {
        private LivroController _controller;
        private Mock<ILivroBusiness> _business;
        private DAL.Models.Produto _livroTDD;
        private DAL.Models.Produto _livroRomance;
        private DAL.Models.Produto _livroFiccao;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _livroTDD = new DAL.Models.Produto
            {
                IdProduto = 1,
                Nome = "TDD desenvolvimento guiado por testes",
                Autor = "Kent Beck",
                Editora = "Bookman",
                Ano = 2010,
                Categoria = "Engenharia de Software",
                Estoque = 0,
                Preco = 50.0M,
                Foto = ""
            };

            _livroRomance = new DAL.Models.Produto
            {
                IdProduto = 2,
                Nome = "O Amor",
                Autor = "Escritora Romance",
                Editora = "Bookman",
                Ano = 2007,
                Categoria = "Ficção",
                Estoque = 0,
                Preco = 30.0M,
                Foto = ""
            };

            _livroFiccao = new DAL.Models.Produto
            {
                IdProduto = 3,
                Nome = "O Senhor Dos Aneis",
                Autor = "Tolken J.R.",
                Editora = "Abril",
                Ano = 2005,
                Categoria = "Ficção",
                Estoque = 0,
                Preco = 100.0M,
                Foto = ""
            };

            _business = new Mock<ILivroBusiness>();
            _business.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            _business.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            _business.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);
            _controller = new LivroController(_business.Object);
        }

        #region US3

        [Test]
        public void QuandoUsuarioFiltarAListaPeloNome_OControleDeveManterOsDadosDoUsuario()
        {
            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            business.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            business.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            var result = _controller.VisualizarLivro(1);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        }

        [Test]
        public void AoSolicitarAVisualizacaoDeUmLivro_OSistemaDeveAbrirAPaginaDeVisualizacaoDeLivro()
        {
            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            business.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            business.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            var result = _controller.VisualizarLivro(1);

            Assert.AreEqual(result.ViewName, "VisualizarLivro");
        }

        [Test]
        public void AoSolicitarAVisualizacaoDeUmLivro_OSistemaDeveBuscarAsInformacoesDoLivroNaCamadaDeNegocios()
        {
            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            business.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            business.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            _controller.VisualizarLivro(1);

            business.Verify(x => x.RecuperarInformacoesDoLivro(1), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarAVisualizacaoDeUmLivro_OSistemaDevePassarAsInformacoesDoLivroParaTela()
        {
            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            business.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            business.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);

            _controller = new LivroController(business.Object);

            var result = _controller.VisualizarLivro(1);

            Assert.Contains("livro", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<ProdutoModel>(result.ViewData["livro"]);
        }

        [Test]
        public void AoSolicitarAlteracaoDeUmLivro_OSistemaDeveEnviarAsInformacoesParaQueACamadaDeNegociosSalve()
        {
            var novosValores = new DAL.Models.Produto
            {
                IdProduto = 3,
                Nome = "A Bela e a Fera",
                Autor = "Popular",
                Editora = "Abril",
                Ano = 2005,
                Categoria = "Infantil",
                Estoque = 10,
                Preco = 10.0M,
                Foto = ""
            };

            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto)).Returns(true);

            var controller = new LivroController(business.Object);

            var result = controller.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            Assert.Contains("livro", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<ProdutoModel>(result.ViewData["livro"]);
            Assert.AreEqual(result.ViewName, "VisualizarLivro");

            Assert.Contains("Sucesso", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase(result.ViewData["Sucesso"] as string, "As informações foram gravadas com sucesso.");
        }

        [Test]
        public void AoSolicitarAlteracaoDeUmLivro_OSistemaDeveSalvarECarregarNovamenteComAsMesmasInformacoes()
        {
            var novosValores = new DAL.Models.Produto
            {
                IdProduto = 3,
                Nome = "A Bela e a Fera",
                Autor = "Popular",
                Editora = "Abril",
                Ano = 2005,
                Categoria = "Infantil",
                Estoque = 10,
                Preco = 10.0M,
                Foto = ""
            };

            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto)).Returns(true);

            var controller = new LivroController(business.Object);

            controller.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto);

            business.Verify(x => x.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto), Times.AtLeastOnce());
        }

        [Test]
        public void AoSolicitarAlteracaoDeUmLivro_OSistemaDeveInformarCasoOcorraAlgumErro()
        {
            var novosValores = new DAL.Models.Produto
            {
                IdProduto = 3,
                Nome = "A Bela e a Fera",
                Autor = "Popular",
                Editora = "Abril",
                Ano = 2005,
                Categoria = "Infantil",
                Estoque = 10,
                Preco = 10.0M,
                Foto = ""
            };

            var business = new Mock<ILivroBusiness>();
            business.Setup(x => x.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto)).Returns(false);

            var controller = new LivroController(business.Object);

            var result = controller.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto);

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            Assert.Contains("livro", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<ProdutoModel>(result.ViewData["livro"]);

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase(result.ViewData["Erro"] as string, "Ocorreu um erro durante o processamento e não foi possível salvar as alterações.");
        }

        #endregion
    }
}