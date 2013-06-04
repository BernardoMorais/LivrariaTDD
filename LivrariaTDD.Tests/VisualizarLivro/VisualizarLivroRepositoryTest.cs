using System;
using System.Collections.Generic;
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
        private DAL.Models.Produto _livroFiccao;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _livroTDD = new Produto
            {
                IdPrduto = 1,
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
                IdPrduto = 2,
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
                IdPrduto = 3,
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
            var id = 1;

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            _repository.RecuperarInformacoesDoLivro(id);

            mockContext.Verify(x => x.Produtos, Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeVisualizacaoDeProdutoEOcorrerUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            var id = 1;

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Throws<Exception>();

            _repository = new ProdutoRepository(mockContext.Object);

            Assert.Throws<Exception>(() => _repository.RecuperarInformacoesDoLivro(id));
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeVisualizacaoDeLivro_OProdutoDeveSerRetornado()
        {
            var id = 1;

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            var result = _repository.RecuperarInformacoesDoLivro(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IProduto>(result);
            Assert.AreEqual(result.IdPrduto, id);
        }
    }
}