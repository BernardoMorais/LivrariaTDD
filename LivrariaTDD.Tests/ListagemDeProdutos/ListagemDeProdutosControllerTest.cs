using System;
using System.Collections;
using System.Collections.Generic;
using LivrariaTDD.Controllers;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.Models;
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
        private DAL.Models.Produto _livroTDD;
        private DAL.Models.Produto _livroRomance;
        private DAL.Models.Produto _livroFiccao;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _livroTDD = new DAL.Models.Produto
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

            _livroRomance = new DAL.Models.Produto
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

            _livroFiccao = new DAL.Models.Produto
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

            _listagemDeProdutosEntity = new List<IProduto>
                {
                  _livroTDD, _livroRomance, _livroFiccao  
                };



            _business = new Mock<IListagemDeProdutosBusiness>();
            _business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);
            _controller = new ListagemDeProdutosController(_business.Object);
        }

        #region US1

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

            _controller.Index();

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
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoCliente_DeveRetornarOTipoDeUsuarioParaPagina()
        {
            var result = _controller.Index(true, "Cliente", "");
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);        
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "Cliente");
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoCliente_DeveRetornarQueExisteUsuarioLogado()
        {
            var result = _controller.Index(true, "Cliente", "");
            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.True((bool)result.ViewData["logado"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoCliente_NaoDeveRetornarErroNaPagina()
        {
            var result = _controller.Index(true, "Cliente", "");
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "");
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoCliente_NaoDeveRetornarOTipoDeUsuarioParaPagina()
        {
            var result = _controller.Index(false, "", "Dados do usuário inválidos.");
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["tipoUsuario"], "");
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoCliente_DeveRetornarQueNaoExisteUsuarioLogado()
        {
            var result = _controller.Index(false, "", "Dados do usuário inválidos.");
            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.False((bool)result.ViewData["logado"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosComSenhaDadosIncorretos_ComoCliente_DeveRetornarUmErroParaPagina()
        {
            var result = _controller.Index(false, "", "Dados do usuário inválidos.");
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase((string)result.ViewData["erroLogin"], "Dados do usuário inválidos.");
        }

        [Test]
        public void QuandoAcessoAPaginaLogado_ComoCliente_APaginaEstaAcessivel()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            Assert.AreEqual("ListagemDeProdutos", result.ViewName);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_APaginaDevePossuirAListagemDeProdutos()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirNome()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Nome);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirAutor()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Autor);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirEditora()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Editora);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirAnoEDeveSerMaiorDoQueZero()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Ano > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirCategoria()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsNotEmpty(produto.Categoria);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirEstoqueENaoDeveSerNegativo()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Estoque >= 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemPossuirPrecoEDeveSerMaiorDoQueZero()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index(true, "Cliente", "");

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.Preco > 0);
            }
        }

        [Test]
        public void AoAcessarAPaginaDeListagemLogado_ComoCliente_OsProdutosDevemVirDaCamadaDeNegocios()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            _controller.Index(true, "Cliente", "");

            business.Verify(x => x.RecuperarTodosProdutos(), Times.AtLeastOnce());
        }

        [Test]
        public void AoAcessarAPaginaDeListagemEOcorrerUmaExcecaoNaCamadaDeNegociosLogado_ComoCliente_OSistemaDeveNotificarAoUsuario()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Throws<Exception>();

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.Index(true, "Cliente", "");

            Assert.Contains("Erro", result.ViewData.Keys as ICollection);
            StringAssert.AreEqualIgnoringCase("Ocorreu um erro durante o processamento. Tente novamente mais tarde.", result.ViewData["Erro"] as string);
        }

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutosLogado_ComoCliente_OsProdutosEnviadosParaTelaDevemSerObjetosDoProjetoMVC()
        {
            var result = _controller.Index(true, "Cliente", "");

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

            _controller.Index(true, "Funcionario", "");

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
        
        #endregion

        #region US2

        [Test]
        public void QuandoUsuarioFiltarAListaPeloNome_OControleDeveRetornarSomenteOsLivrosComNomesCorrepondentes()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.PesquisaProduto("TDD", "");

            var lista = (List<ProdutoModel>) result.ViewData["ListagemDeProdutos"];

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
            StringAssert.AreEqualIgnoringCase(lista[0].Nome, _livroTDD.Nome);
            Assert.AreEqual(lista.Count, 1);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaPelaCategoria_OControleDeveRetornarSomenteOsLivrosComCategoriasCorrepondentes()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.PesquisaProduto("", "Ficção");

            var lista = (List<ProdutoModel>)result.ViewData["ListagemDeProdutos"];

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
            StringAssert.AreEqualIgnoringCase(lista[0].Nome, _livroRomance.Nome);
            StringAssert.AreEqualIgnoringCase(lista[1].Nome, _livroFiccao.Nome);
            Assert.AreEqual(lista.Count, 2);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaPeloNomeECategoria_OControleDeveRetornarSomenteOsLivrosComNomesECategoriaCorrepondentes()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.PesquisaProduto("Aneis","Ficção");

            var lista = (List<ProdutoModel>)result.ViewData["ListagemDeProdutos"];

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
            StringAssert.AreEqualIgnoringCase(lista[0].Nome, _livroFiccao.Nome);
            Assert.AreEqual(lista.Count, 1);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaComParametrosVazio_OControleDeveRetornarTodosOsLivros()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.PesquisaProduto("", "");

            var lista = (List<ProdutoModel>)result.ViewData["ListagemDeProdutos"];

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.IsInstanceOf<List<ProdutoModel>>(result.ViewData["ListagemDeProdutos"]);
            StringAssert.AreEqualIgnoringCase(lista[0].Nome, _livroTDD.Nome);
            StringAssert.AreEqualIgnoringCase(lista[1].Nome, _livroRomance.Nome);
            StringAssert.AreEqualIgnoringCase(lista[2].Nome, _livroFiccao.Nome);
            Assert.AreEqual(lista.Count, 3);
        }

        [Test]
        public void QuandoUsuarioFiltarAListaPeloNome_OControleDeveManterOsDadosDoUsuario()
        {
            var business = new Mock<IListagemDeProdutosBusiness>();
            business.Setup(x => x.RecuperarTodosProdutos()).Returns(_listagemDeProdutosEntity);

            _controller = new ListagemDeProdutosController(business.Object);

            var result = _controller.PesquisaProduto("TDD", "");

            var lista = (List<ProdutoModel>)result.ViewData["ListagemDeProdutos"];

            Assert.Contains("ListagemDeProdutos", result.ViewData.Keys as ICollection);
            Assert.Contains("logado", result.ViewData.Keys as ICollection);
            Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
            Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        }

        #endregion

        #region US3

        [Test]
        public void AoAcessarAPaginaDeListagemDeProdutos_OsProdutosDevemPossuirIdENaoDeveSerNegativo()
        {
            _controller = new ListagemDeProdutosController(_business.Object);

            var result = _controller.Index();

            var list = result.ViewData["ListagemDeProdutos"] as List<ProdutoModel>;

            Assert.IsNotNull(list);

            Assert.IsNotEmpty(list);

            foreach (var produto in list)
            {
                Assert.IsTrue(produto.IdPrduto >= 0);
            }
        }

        #endregion
    }
}