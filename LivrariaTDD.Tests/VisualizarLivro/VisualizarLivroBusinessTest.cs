using System;
using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.BRL.Livro;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroBusinessTest
    {
        private ILivroBusiness _business;
        private Mock<IProdutoRepository> _repository;
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

            _repository = new Mock<IProdutoRepository>();
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);
            _business = new LivroBusiness(_repository.Object);
        }
        
        #region US3

        [Test]
        public void AoAcessarACamadaDeNegociosParaVisualizarUmLivro_OLivroDeveVirDaCamadaDeAcessoADados()
        {
            const int id = 1;

            var result = _business.RecuperarInformacoesDoLivro(id);

            _repository.Verify(x => x.RecuperarInformacoesDoLivro(id), Times.AtLeastOnce());
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IProduto>(result);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeVisualizacaoEOcorrerUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            const int id = 1;

            var repository = new Mock<IProdutoRepository>();

            repository.Setup(x => x.RecuperarInformacoesDoLivro(id)).Throws<Exception>();

            var business = new LivroBusiness(repository.Object);

            Assert.Throws<Exception>(() => business.RecuperarInformacoesDoLivro(id));
        }

        [Test]
        public void AoAcessarACamadaDeNegociosParaAlterarUmLivro_OLivroDeveSerEnviadoParaSeSalvoNaCamadaDeAcessoADados()
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

            _repository.Setup(x => x.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto)).Returns(true);

            var result = _business.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto);

            _repository.Verify(x => x.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto), Times.AtLeastOnce());
            Assert.True(result);
        }

        #endregion
    }
}