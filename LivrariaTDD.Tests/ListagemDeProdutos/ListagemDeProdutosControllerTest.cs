using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Controllers.Home;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Models.Product;
using Moq;
using NUnit.Framework;
using Product = LivrariaTDD.Infrastructure.Models.Product;

namespace LivrariaTDD.MVCTests.ListagemDeProdutos
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _controller;
        private Mock<IProductBusiness> _business;
        private List<Product> _listagemDeProdutosEntity;
        private Product _livroTDD;
        private Product _livroRomance;
        private Product _livroFiccao;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _livroTDD = new Product
                {
                    ProductId = 1,
                    Name = "TDD desenvolvimento guiado por testes",
                    Author = "Kent Beck",
                    Publishing = "Bookman",
                    Year = 2010,
                    Category = Categories.LiteraturaEstrangeira,
                    Stock = 0,
                    Price = 50.0M,
                    Photo = ""
                };

            _livroRomance = new Product
            {
                ProductId = 2,
                Name = "O Amor",
                Author = "Escritora Romance",
                Publishing = "Bookman",
                Year = 2007,
                Category = Categories.LiteraturaBrasileira,
                Stock = 0,
                Price = 30.0M,
                Photo = ""
            };

            _livroFiccao = new Product
            {
                ProductId = 3,
                Name = "O Senhor Dos Aneis",
                Author = "Tolken J.R.",
                Publishing = "Abril",
                Year = 2005,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 0,
                Price = 100.0M,
                Photo = ""
            };

            _listagemDeProdutosEntity = new List<Product>
                {
                  _livroTDD, _livroRomance, _livroFiccao  
                };



            _business = new Mock<IProductBusiness>();
            _business.Setup(x => x.GetActiveProducts()).Returns(_listagemDeProdutosEntity);
            _controller = new HomeController(_business.Object);
        }

        #region US1

        [Test]
        public void QuandoAcessoAPagina_APaginaEstarAcessivel()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirName()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result);

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsFalse(String.IsNullOrEmpty(produto.Name));
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirAuthor()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Author);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirPublishing()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Publishing);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirYearEDeveSerMaiorDoQueZero()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Year > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirCategory()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotNull(produto.Category);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirStockENaoDeveSerNegativo()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Stock >= 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_ComoFuncionarioDaLoja_OsProdutosDevemPossuirPriceEDeveSerMaiorDoQueZero()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Price > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemEOcorrerUmaExcecaoNaCamadaDeNegocios_ComoFuncionarioDaLoja_OSistemaDeveNotificarAoUsuario()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Throws<Exception>();

            _controller = new HomeController(business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase("Ocorreu um erro durante o processamento. Tente novamente mais tarde.", result.ViewData["Erro"] as string);
        }

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutos_DeveRetornarOTipoDeUsuarioParaPagina()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(true, "Cliente", "");
        //    Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);        
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "Cliente");
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutos_DeveRetornarQueExisteUsuarioLogado()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(true, "Cliente", "");
        //    Assert.Contains("logado", result.ViewData.Keys as ICollection);
        //    Assert.True((bool)result.ViewData["logado"]);
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutos_NaoDeveRetornarErroNaPagina()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(true, "Cliente", "");
        //    Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "");
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoCliente_NaoDeveRetornarOTipoDeUsuarioParaPagina()
        //{
        //    var result = _controller.Entrar(false, "", "Dados do usuário inválidos.");
        //    Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "");
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoCliente_DeveRetornarQueNaoExisteUsuarioLogado()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(false, "", "Dados do usuário inválidos.");
        //    Assert.Contains("logado", result.ViewData.Keys as ICollection);
        //    Assert.False((bool)result.ViewData["logado"]);
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoCliente_DeveRetornarUmErroParaPagina()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(false, "", "Dados do usuário inválidos.");
        //    Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "Dados do usuário inválidos.");
        //}

        //[Test]
        //public void QuandoAcessoAPagina_APaginaEstaAcessivel()
        //{
        //    _controller = new HomeController(_business.Object);

        //    var result = _controller.Entrar(true, "Cliente", "");

        //    Assert.AreEqual("ListagemDeProdutos", result.ViewName);
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagem_APaginaDevePossuirAListagemDeProdutos()
        //{
        //    _controller = new HomeController(_business.Object);

        //    var result = _controller.Entrar(true, "Cliente", "");

        //    Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
        //    Assert.IsInstanceOf<List<Models.Product.Product>>(result.ViewData["ListagemDeProdutos"]);
        //}

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirName()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Name);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirPublishing()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Publishing);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirYearEDeveSerMaiorDoQueZero()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Year > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirCategory()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotNull(produto.Category);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirStockENaoDeveSerNegativo()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Stock >= 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemPossuirPriceEDeveSerMaiorDoQueZero()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Price > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagem_OsProdutosDevemVirDaCamadaDeNegocios()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Returns(_listagemDeProdutosEntity);

            _controller = new HomeController(business.Object);

            _controller.Index();

            business.Verify(x => x.GetActiveProducts(), Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarAPaginaDeListagemEOcorrerUmaExcecaoNaCamadaDeNegocios_OSistemaDeveNotificarAoUsuario()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Throws<Exception>();

            _controller = new HomeController(business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); 

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase("Ocorreu um erro durante o processamento. Tente novamente mais tarde.", result.ViewData["Erro"] as string);
        }

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutos_DeveRetornarOTipoDeUsuarioParaPagina()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(true, "Funcionario", "");
        //    Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "Funcionario");
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutos_DeveRetornarQueExisteUsuarioLogado()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(true, "Funcionario", "");
        //    Assert.Contains("logado", result.ViewData.Keys as ICollection);
        //    Assert.True((bool)result.ViewData["logado"]);
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutos_NaoDeveRetornarErroNaPagina()
        //{
        //    _controller = new HomeController(_business.Object);
        //    var result = _controller.Entrar(true, "Funcionario", "");
        //    Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "");
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoFuncionarioDaLoja_NaoDeveRetornarOTipoDeUsuarioParaPagina()
        //{
        //    var result = _controller.Entrar(false, "", "Dados do usuário inválidos.");
        //    Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "");
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoFuncionarioDaLoja_DeveRetornarQueNaoExisteUsuarioLogado()
        //{
        //    var result = _controller.Entrar(false, "", "Dados do usuário inválidos.");
        //    Assert.Contains("logado", result.ViewData.Keys as ICollection);
        //    Assert.False((bool)result.ViewData["logado"]);
        //}

        //[Test]
        //public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoFuncionarioDaLoja_DeveRetornarUmErroParaPagina()
        //{
        //    var result = _controller.Entrar(false, "", "Dados do usuário inválidos.");
        //    Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        //    StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "Dados do usuário inválidos.");
        //}

        //[Test]
        //public void QuandoAcessoAPagina_APaginaEstaAcessivel()
        //{
        //    _controller = new HomeController(_business.Object);

        //    var result = _controller.Entrar(true, "Funcionario", "");

        //    Assert.AreEqual("ListagemDeProdutos", result.ViewName);
        //}

        [Test]
        public void AoAcessarAPaginaDeListagem_APaginaDevePossuirAListagemDeProdutos()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ProductList>(result.Model);
        }

        #endregion

        #region US2

        [Test]
        public void QuandoUsuarioFiltarAListaPeloName_OControleDeveRetornarSomenteOsLivrosComNamesCorrepondentes()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Returns(_listagemDeProdutosEntity);

            _controller = new HomeController(business.Object);

            var result = _controller.Search("TDD", null);

            var lista = ((ProductList)result.Model).Products;

            Assert.IsInstanceOf<ProductList>(result.Model);
            StringAssert.AreEqualIgnoringCase(lista[0].Name, _livroTDD.Name);
            Assert.AreEqual(lista.Count, 1);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaPelaCategory_OControleDeveRetornarSomenteOsLivrosComCategorysCorrepondentes()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Returns(_listagemDeProdutosEntity);

            _controller = new HomeController(business.Object);

            var result = _controller.Search("", Categories.LiteraturaEstrangeira);

            var lista = ((ProductList)result.Model).Products;

            Assert.IsInstanceOf<ProductList>(result.Model);
            StringAssert.AreEqualIgnoringCase(lista[0].Name, _livroTDD.Name);
            StringAssert.AreEqualIgnoringCase(lista[1].Name, _livroFiccao.Name);
            Assert.AreEqual(lista.Count, 2);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaPeloNameECategory_OControleDeveRetornarSomenteOsLivrosComNamesECategoryCorrepondentes()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Returns(_listagemDeProdutosEntity);

            _controller = new HomeController(business.Object);

            var result = _controller.Search("Aneis", Categories.LiteraturaEstrangeira);

            var lista = ((ProductList)result.Model).Products;

            Assert.IsInstanceOf<ProductList>(result.Model);
            StringAssert.AreEqualIgnoringCase(lista[0].Name, _livroFiccao.Name);
            Assert.AreEqual(lista.Count, 1);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaComParametrosVazio_OControleDeveRetornarTodosOsLivros()
        {
            var business = new Mock<IProductBusiness>();
            business.Setup(x => x.GetActiveProducts()).Returns(_listagemDeProdutosEntity);

            _controller = new HomeController(business.Object);

            var result = _controller.Search("", null);

            var lista = ((ProductList)result.Model).Products;

            Assert.IsInstanceOf<ProductList>(result.Model);
            StringAssert.AreEqualIgnoringCase(lista[0].Name, _livroTDD.Name);
            StringAssert.AreEqualIgnoringCase(lista[1].Name, _livroRomance.Name);
            StringAssert.AreEqualIgnoringCase(lista[2].Name, _livroFiccao.Name);
            Assert.AreEqual(lista.Count, 3);
        }

        #endregion

        #region US3

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutos_OsProdutosDevemPossuirIdENaoDeveSerNegativo()
        {
            _controller = new HomeController(_business.Object);

            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result); var list = ((ProductList)result.Model).Products;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.ProductId >= 0);
            }
        }

        #endregion
    }
}