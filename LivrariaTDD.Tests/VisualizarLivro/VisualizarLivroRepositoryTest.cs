using System;
using System.Linq;
using LivrariaTDD.DAL.Models;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroRepositoryTest
    {
        private IProdutoRepository _repository;
        private EnumerableQuery<IProduto> _listaDeProdutos;
        private Produto _livroTDD;
        private Produto _livroRomance;
        private Produto _livroFiccao;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _livroTDD = new Produto
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

            _livroRomance = new Produto
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

            _livroFiccao = new Produto
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

            _listaDeProdutos = new EnumerableQuery<IProduto>(new[]
                {
                    _livroTDD,_livroRomance,_livroFiccao
                });
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeVisualizacaoDeProduto_OProdutoDeveVirDaDoFrameworkDeORM()
        {
            const int id = 1;

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            _repository.RecuperarInformacoesDoLivro(id);

            mockContext.Verify(x => x.Produtos, Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeVisualizacaoDeProdutoEOcorrerUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            const int id = 1;

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Throws<Exception>();

            _repository = new ProdutoRepository(mockContext.Object);

            Assert.Throws<Exception>(() => _repository.RecuperarInformacoesDoLivro(id));
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeVisualizacaoDeLivro_OProdutoDeveSerRetornado()
        {
            const int id = 1;

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            var result = _repository.RecuperarInformacoesDoLivro(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IProduto>(result);
            Assert.AreEqual(result.IdProduto, id);
        }

        [Test]
        public void AoAcessarACamdadaDeAcessoADadosParaAlterarUmProduto_OProdutoDeveAcessarOFrameworkDeORM()
        {
            var novosValores = new Produto
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

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            _repository.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto);

            mockContext.Verify(x => x.Produtos, Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarACamdadaDeAcessoADadosParaAlterarUmProduto_OProdutoDeveSerSalvoEUmResultadoBooleanoDeveSerRetornado()
        {
            var novosValores = new Produto
            {
                IdProduto = 3,
                Nome = "Cinderela",
                Autor = "Popular",
                Editora = "Abril",
                Ano = 2005,
                Categoria = "Infantil",
                Estoque = 10,
                Preco = 10.0M,
                Foto = ""
            };

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            var aux = _repository.RecuperarInformacoesDoLivro(novosValores.IdProduto);

            var livroAntigo = new Produto
                {
                    IdProduto = aux.IdProduto,
                    Nome = aux.Nome,
                    Autor = aux.Autor,
                    Editora = aux.Editora,
                    Ano = aux.Ano,
                    Categoria = aux.Categoria,
                    Estoque = aux.Estoque,
                    Preco = aux.Preco,
                    Foto = aux.Foto
                };

            var result = _repository.AlterarLivro(novosValores.IdProduto, novosValores.Nome, novosValores.Autor, novosValores.Editora, novosValores.Ano, novosValores.Categoria, novosValores.Estoque, novosValores.Preco, novosValores.Foto);

            var livroNovo = _repository.RecuperarInformacoesDoLivro(novosValores.IdProduto);

            Assert.True(result);
            Assert.AreEqual(livroAntigo.IdProduto, livroNovo.IdProduto);
            StringAssert.AreNotEqualIgnoringCase(livroAntigo.Nome, livroNovo.Nome);
        }
    }
}