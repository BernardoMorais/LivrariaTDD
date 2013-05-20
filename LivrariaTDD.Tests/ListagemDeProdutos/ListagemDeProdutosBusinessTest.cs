using System;
using System.Collections.Generic;
using LivrariaTDD.BRL;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.ListagemDeProdutos
{
    [TestFixture]
    public class ListagemDeProdutosBusinessTest
    {
        private IListagemDeProdutosBusiness _business;
        private Mock<IProdutoRepository> _repository;
        private List<IProduto> _listagemDeProdutosEntity;

        [TestFixtureSetUp]
        public void SetUp()
        {

            _listagemDeProdutosEntity = new List<IProduto>
                {
                    new DAL.Models.Produto { Nome = "TDD desenvolvimento guiado por testes", Autor = "Kent Beck", Editora = "Bookman", Ano = 2010, Categoria = "Engenharia de Software", Estoque = 0, Preco = 50.0, Foto = "" }
                };

            _repository = new Mock<IProdutoRepository>();
            _repository.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);
            _business = new ListagemDeProdutosBusiness(_repository.Object);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirDaCamadaDeAcessoADados()
        {
            var result = _business.RecuperarTodosProdutos();

            _repository.Verify(x => x.RecuperarTodosProdutos(), Times.AtLeastOnce());
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<IProduto>>(result);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagemEOcorreUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(x => x.RecuperarTodosProdutos()).Throws<Exception>();

            var business = new ListagemDeProdutosBusiness(repository.Object);

            Assert.Throws<Exception>(() => business.RecuperarTodosProdutos());
        }
    }
}