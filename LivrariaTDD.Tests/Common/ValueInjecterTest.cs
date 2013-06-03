using System;
using System.Collections;
using System.Collections.Generic;
using LivrariaTDD.Controllers;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.Models;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;
using Omu.ValueInjecter;

namespace LivrariaTDD.MVCTests.Common
{
    [TestFixture]
    public class ValueINjecterTest
    {
        private List<IProduto> _listagemDeProdutosEntity;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _listagemDeProdutosEntity = new List<IProduto>
                {
                    new LivrariaTDD.DAL.Models.Produto { Nome = "TDD desenvolvimento guiado por testes", Autor = "Kent Beck", Editora = "Bookman", Ano = 2010, Categoria = "Engenharia de Software", Estoque = 0, Preco = 50.0M, Foto = "" }
                };
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutos_ComoFuncionarioDaLoja_OsProdutosEnviadosParaTelaDevemSerObjetosDoProjetoMVC()
        {
            var novaLista = new List<ProdutoModel>();

            foreach (var produto in _listagemDeProdutosEntity)
            {
                var novoProduto = new ProdutoModel();
                novoProduto.InjectFrom(produto);
                novaLista.Add(novoProduto);
            }

            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Nome, novaLista[0].Nome);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Autor, novaLista[0].Autor);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Editora, novaLista[0].Editora);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Ano, novaLista[0].Ano);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Categoria, novaLista[0].Categoria);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Estoque, novaLista[0].Estoque);
            Assert.AreEqual(_listagemDeProdutosEntity[0].Preco, novaLista[0].Preco);
            StringAssert.AreEqualIgnoringCase(_listagemDeProdutosEntity[0].Foto, novaLista[0].Foto);
        }
    }
}