using System;
using System.Collections.Generic;
using LivrariaTDD.BRL.Livro;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.ListagemDeProdutos
{
    [TestFixture]
    public class ListagemDeProdutosBusinessTest
    {
        private IProductBusiness _business;
        private Mock<IProductRepository> _repository;
        private List<Product> _listagemDeProdutosEntity;

        [TestFixtureSetUp]
        public void SetUp()
        {

            _listagemDeProdutosEntity = new List<Product>
                {
                    new Product { Name = "TDD desenvolvimento guiado por testes", Author = "Kent Beck", Publishing = "Bookman", Year = 2010, Category = Categories.LiteraturaEstrangeira, Stock = 0, Price = 50.0M, Photo = "" }
                };

            _repository = new Mock<IProductRepository>();
            _repository.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);
            _business = new ProductBusiness(_repository.Object);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirDaCamadaDeAcessoADados()
        {
            var result = _business.GetAll();

            _repository.Verify(x => x.RecuperarTodosProdutos(), Times.AtLeastOnce());
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagemEOcorreUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            var repository = new Mock<IProductRepository>();
            repository.Setup(x => x.RecuperarTodosProdutos()).Throws<Exception>();

            var business = new ProductBusiness(repository.Object);

            Assert.Throws<Exception>(() => business.GetAll());
        }
    }
}