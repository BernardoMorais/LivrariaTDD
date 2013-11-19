using System.Collections.Generic;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;
using Omu.ValueInjecter;

namespace LivrariaTDD.MVCTests.Common
{
    [TestFixture]
    public class ValueINjecterTest
    {
        private List<Product> _listagemDeProdutosEntity;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _listagemDeProdutosEntity = new List<Product>
                {
                    new Product { Name = "TDD desenvolvimento guiado por testes", Author = "Kent Beck", Publishing = "Bookman", Year = 2010, Category = Categories.LiteraturaEstrangeira, Stock = 0, Price = 50.0M, Photo = "" }
                };
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutos_ComoFuncionarioDaLoja_OsProdutosEnviadosParaTelaDevemSerObjetosDoProjetoMVC()
        {
            var novaLista = new List<Models.Product.Product>();

            foreach (var produto in _listagemDeProdutosEntity)
            {
                var novoProduto = new Models.Product.Product();
                novoProduto.InjectFrom(produto);
                novaLista.Add(novoProduto);
            }

            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Name, novaLista[0].Name);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Author, novaLista[0].Author);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Publishing, novaLista[0].Publishing);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Year, novaLista[0].Year);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Category, novaLista[0].Category);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Stock, novaLista[0].Stock);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Price, novaLista[0].Price);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Photo, novaLista[0].Photo);
        }
    }
}