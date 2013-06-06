using LivrariaTDD.BRL.Livro;
using LivrariaTDD.DAL.Models;
using LivrariaTDD.Infrastructure.DAL.Repository;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.CadastrarLivro
{
    public class CadastrarLivroBusinessTest
    {
        private LivroBusiness _business;
        private Mock<IProdutoRepository> _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Mock<IProdutoRepository>();
            _business = new LivroBusiness(_repository.Object);
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitaCadastroDeUmNovoLivro_ACamadaDeNegociosDeveAcessarACamadaDeAcessoADadosParaSalvarOLivro()
        {
            var novoLivro = new Produto
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

            _repository.Setup(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto)).Returns(true);

            _business.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria,
                                  novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            _repository.Verify(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto),Times.AtLeastOnce());
        }

        [Test]
        public void QuandoACamadaDeNegociosSalvaUmLivroComSucesso_DeveRetornarVedadeiroParaQuemAChamou()
        {
            var novoLivro = new Produto
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

            _repository.Setup(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto)).Returns(true);

            var result =_business.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria,
                                  novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            Assert.True(result);
        }

        [Test]
        public void QuandoACamadaDeNegocioNaoSalvaUmLivroPorAlgumaFalha_DeveRetornarFalsoParaQuemAChamou()
        {
            var novoLivro = new Produto
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

            _repository.Setup(x => x.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto)).Returns(false);

            var result = _business.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria,
                                  novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            Assert.False(result);
        }
    }
}
