﻿using System;
using System.Collections;
using System.Collections.Generic;
using LivrariaTDD.Controllers;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.Models;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.ListagemDeProdutos
{
    [TestFixture]
    public class ListagemDeProdutosControllerTest
    {
        private ListagemDeProdutosController _controller;
        private Mock<IListagemDeProdutosBusiness> _business;        
        private List<IProduto> _listagemDeProdutosEntity;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _listagemDeProdutosEntity = new List<IProduto>
                {
                    new LivrariaTDD.DAL.Models.Produto { Nome = "TDD desenvolvimento guiado por testes", Autor = "Kent Beck", Editora = "Bookman", Ano = 2010, Categoria = "Engenharia de Software", Estoque = 0, Preco = 50.0M, Foto = "" }
                };

            _business = new Mock<IListagemDeProdutosBusiness>();
            _business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);
            _controller = new ListagemDeProdutosController(_business.Object);
        }

        [Test]
        public void QuandoAcessoAPagina_ComoFuncionarioDaLoja_APaginaEstaAcessivel()
        {
            var result = _controller.Index();

            Assert.AreEqual("ListagemDeProdutos", result.ViewName);
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_APaginaDevePossuirAListagemDeProdutos()
        {
            var result = _controller.Index();

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirNome()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Nome);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirAutor()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Autor);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirEditora()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Editora);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirAnoEDeveSerMaiorDoQueZero()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Ano > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirCategoria()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Categoria);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirEstoqueENaoDeveSerNegativo()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Estoque >= 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirPrecoEDeveSerMaiorDoQueZero()
        {
            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Preco > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemVirDaCamadaDeNegocios()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.Index();

            business.Verify(x => x.RecuperarTodosProdutos(), Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarAPaginaDeListagemEOcorrerUmaExcecaoNaCamadaDeNegocios_ComoFuncionarioDaLoja_OSistemaDeveNotificarAoUsuario()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Throws<Exception>();

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.Index();

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);            
            StringAssert.AreEqualIgnoringCase("Ocorreu um erro durante o processamento. Tente novamente mais tarde.", result.ViewData["Erro"] as string);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutos_ComoFuncionarioDaLoja_OsProdutosEnviadosParaTelaDevemSerObjetosDoProjetoMVC()
        {
            var result = _controller.Index();

            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoFuncionarioDaLoja_DeveRetornarOTipoDeUsuarioParaPagina()
        {
            var result = _controller.Index(true, "Funcionario", "");
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);        
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "Funcionario");
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoFuncionarioDaLoja_DeveRetornarQueExisteUsuarioLogado()
        {
            var result = _controller.Index(true, "Funcionario", "");
            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.True((bool)result.ViewData["logado"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoFuncionarioDaLoja_NaoDeveRetornarErroNaPagina()
        {
            var result = _controller.Index(true, "Funcionario", "");
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "");
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoFuncionarioDaLoja_NaoDeveRetornarOTipoDeUsuarioParaPagina()
        {
            var result = _controller.Index(false, "", "Dados do usuário inválidos.");
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "");
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoFuncionarioDaLoja_DeveRetornarQueNaoExisteUsuarioLogado()
        {
            var result = _controller.Index(false, "", "Dados do usuário inválidos.");
            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.False((bool)result.ViewData["logado"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoFuncionarioDaLoja_DeveRetornarUmErroParaPagina()
        {
            var result = _controller.Index(false, "", "Dados do usuário inválidos.");
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "Dados do usuário inválidos.");
        }

        [Test]
        public void QuandoAcessoAPaginaLogado_ComoFuncionarioDaLoja_APaginaEstaAcessivel()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            Assert.AreEqual("ListagemDeProdutos", result.ViewName);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_APaginaDevePossuirAListagemDeProdutos()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirNome()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Nome);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirAutor()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Autor);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirEditora()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Editora);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirAnoEDeveSerMaiorDoQueZero()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Ano > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirCategoria()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Categoria);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirEstoqueENaoDeveSerNegativo()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Estoque >= 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemPossuirPrecoEDeveSerMaiorDoQueZero()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Preco > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoFuncionarioDaLoja_OsProdutosDevemVirDaCamadaDeNegocios()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            business.Verify(x => x.RecuperarTodosProdutos(), Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarAPaginaDeListagemEOcorrerUmaExcecaoNaCamadaDeNegociosLogado_ComoFuncionarioDaLoja_OSistemaDeveNotificarAoUsuario()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Throws<Exception>();

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.Index(true, "Funcionario", "");

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase("Ocorreu um erro durante o processamento. Tente novamente mais tarde.", result.ViewData["Erro"] as string);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoFuncionarioDaLoja_OsProdutosEnviadosParaTelaDevemSerObjetosDoProjetoMVC()
        {
            var result = _controller.Index(true, "Funcionario", "");

            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
        }
    }
}