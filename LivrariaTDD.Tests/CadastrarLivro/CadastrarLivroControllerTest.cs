using System.Collections;
using LivrariaTDD.Controllers.Livros;
using LivrariaTDD.Infrastructure.BRL.Livro;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.CadastrarLivro
{
    [TestFixture]
    public class CadastrarLivroControllerTest
    {
        private LivroController _controller;
        private Mock<ILivroBusiness> _business;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _business = new Mock<ILivroBusiness>();
            _controller = new LivroController(_business.Object);
        }

        [Test]
        public void QuandoUsuarioSolicitarCadastroDeUmNovoLivro_OControleDeveManterOsDadosDoUsuario()
        {
            _controller = new LivroController(_business.Object);

            var result = _controller.CadastrarLivro();

            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        }

        [Test]
        public void QuandoUsuarioSolicitarCadastroDeUmNovoLivro_OControleDeveEncaminharOUsuarioParaPaginaDeCadastroDeLivro()
        {
            _controller = new LivroController(_business.Object);

            var result = _controller.CadastrarLivro();

            StringAssert.AreEqualIgnoringCase("CadastrarLivro", result.ViewName);
        }

        [Test]
        public void QuandoUsuarioDigitarEOsDadosDoNovoLivroEClicarEmCadastrar_OControleDeveEnviarOsDadosParaQueACamadaDeNegociosSalveONovoLivro()
        {
            var novoLivro = new ProdutoModel
                {
                    Nome = "Torre Negra",
                    Autor = "Stephen King",
                    Editora = "Universal",
                    Ano = 1995,
                    Categoria = "Ficção",
                    Estoque = 5,
                    Preco = 150.0M,
                    Foto = ""
                };

            _business.Setup(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto)).Returns(true);

            _controller = new LivroController(_business.Object);

            _controller.CadastrarLivro(novoLivro);

            _business.Verify(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto), Times.AtLeastOnce());
        }

        [Test]
        public void CasoACamadaDeNegociosSalveOLivroComSucesso_OControleDeveEnviarUmaMensagemDeSucessoERetornarParaPaginaDeListagem()
        {
            var novoLivro = new ProdutoModel
                {
                Nome = "Torre Negra",
                Autor = "Stephen King",
                Editora = "Universal",
                Ano = 1995,
                Categoria = "Ficção",
                Estoque = 5,
                Preco = 150.0M,
                Foto = ""
            };

            _business.Setup(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto)).Returns(true);

            _controller = new LivroController(_business.Object);

            var result = _controller.CadastrarLivro(novoLivro);

            StringAssert.AreEqualIgnoringCase("ListagemDeProdutos",result.ViewName);
            CollectionAssert.Contains(result.ViewData.Keys, "Sucesso");
            CollectionAssert.DoesNotContain(result.ViewData.Keys, "Falha");
        }

        [Test]
        public void CasoACamadaDeNegociosNaoSalveOLivroComSucesso_OControleDeveEnviarUmaMensagemDeFalhaEContinuarNaPaginaDeCadastro()
        {
            var novoLivro = new ProdutoModel
                {
                Nome = "Torre Negra",
                Autor = "Stephen King",
                Editora = "Universal",
                Ano = 1995,
                Categoria = "Ficção",
                Estoque = 5,
                Preco = 150.0M,
                Foto = ""
            };

            _business.Setup(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto)).Returns(false);

            _controller = new LivroController(_business.Object);

            var result = _controller.CadastrarLivro(novoLivro);

            StringAssert.AreEqualIgnoringCase("CadastrarLivro", result.ViewName);
            StringAssert.AreEqualIgnoringCase(novoLivro.Nome, ((ProdutoModel) result.Model).Nome);
            CollectionAssert.DoesNotContain(result.ViewData.Keys, "Sucesso");
            CollectionAssert.Contains(result.ViewData.Keys, "Falha");
        }
    }
}
