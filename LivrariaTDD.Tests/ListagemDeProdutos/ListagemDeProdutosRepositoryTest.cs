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

namespace LivrariaTDD.MVCTests.ListagemDeProdutos
{
    [TestFixture]
    public class ListagemDeProdutosRepositoryTest
    {
        private IProdutoRepository _repository;
        private EnumerableQuery<IProduto> _listaDeProdutos;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _listaDeProdutos = new EnumerableQuery<IProduto>(new[]
                {
                    new Produto
                        {
                            Nome = "TDD desenvolvimento guiado por testes",
                            Autor = "Kent Beck",
                            Editora = "Bookman",
                            Ano = 2010,
                            Categoria = "Engenharia de Software",
                            Estoque = 0,
                            Preco = 50.0M,
                            Foto = ""
                        }
                });
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirDaDoFrameworkDeORM()
        {
            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            _repository.RecuperarTodosProdutos();

            mockContext.Verify(x => x.Produtos, Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosDaPaginaDeListagemDaPaginaDeListagemEOcorreUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Throws<Exception>();

            _repository = new ProdutoRepository(mockContext.Object);

            Assert.Throws<Exception>(() => _repository.RecuperarTodosProdutos());
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirDaCamadaDeAcessoADados()
        {
            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            var result = _repository.RecuperarTodosProdutos();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<IProduto>>(result);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.IsNotEmpty(result);
        }
    }
}