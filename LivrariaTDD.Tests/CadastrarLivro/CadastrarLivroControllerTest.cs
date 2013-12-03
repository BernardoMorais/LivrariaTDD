using System.Collections;
using LivrariaTDD.Controllers.Product;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using LivrariaTDD.Models;
using Moq;
using NUnit.Framework;
using Omu.ValueInjecter;

namespace LivrariaTDD.MVCTests.CadastrarLivro
{
    [TestFixture]
    public class CadastrarProductControllerTest
    {
        private ProductController _controller;
        private Mock<IProductBusiness> _business;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _business = new Mock<IProductBusiness>();
            _controller = new ProductController(_business.Object);
        }

        //[Test]
        //public void QuandoUsuarioSolicitarCadastroDeUmNovoLivro_OControleDeveManterOsDadosDoUsuario()
        //{
        //    _controller = new ProductController(_business.Object);

        //    var result = _controller.CadastrarLivro();

        //    Assert.Contains("logado", result.ViewData.Keys as ICollection);
        //    Assert.Contains("tipoUsuario", result.ViewData.Keys as ICollection);
        //    Assert.Contains("erroLogin", result.ViewData.Keys as ICollection);
        //}

        //[Test]
        //public void QuandoUsuarioSolicitarCadastroDeUmNovoLivro_OControleDeveEncaminharOUsuarioParaPaginaDeCadastroDeLivro()
        //{
        //    _controller = new ProductController(_business.Object);

        //    var result = _controller.CadastrarLivro();

        //    StringAssert.AreEqualIgnoringCase("CadastrarLivro", result.ViewName);
        //}

        //[Test]
        //public void QuandoUsuarioDigitarOsDadosDoNovoLivroEClicarEmCadastrar_OControleDeveEnviarOsDadosParaQueACamadaDeNegociosSalveONovoLivro()
        //{
        //    var novoLivro = new Models.Product.Product
        //        {
        //            ProductId = 1,
        //            Name = "Torre Negra",
        //            Author = "Stephen King",
        //            Publishing = "Universal",
        //            Year = 1995,
        //            Category = Categories.LiteraturaEstrangeira,
        //            Stock = 5,
        //            Price = 150.0M,
        //            Photo = ""
        //        };

        //    _business.Setup(x => x.SalvarLivro(It.IsAny<Product>())).Returns(new Product());

        //    _controller = new ProductController(_business.Object);

        //    _controller.CadastrarLivro(novoLivro);

        //    _business.Verify(x => x.SalvarLivro(It.IsAny<Product>()), Times.AtLeastOnce());
        //}

        //[Test]
        //public void CasoACamadaDeNegociosSalveOLivroComSucesso_OControleDeveEnviarUmaMensagemDeSucessoERetornarParaPaginaDeListagem()
        //{
        //    var novoLivro = new Models.Product.Product
        //        {
        //        ProductId = 1,
        //        Name = "Torre Negra",
        //        Author = "Stephen King",
        //        Publishing = "Universal",
        //        Year = 1995,
        //        Category = Categories.LiteraturaEstrangeira,
        //        Stock = 5,
        //        Price = 150.0M,
        //        Photo = ""
        //    };

        //    var livro = new Product();
        //    livro.InjectFrom(novoLivro);

        //    _business.Setup(x => x.SalvarLivro(It.IsAny<Product>())).Returns(livro);

        //    _controller = new ProductController(_business.Object);

        //    var result = _controller.CadastrarLivro(novoLivro);

        //    StringAssert.AreEqualIgnoringCase("ListagemDeProdutos",result.ViewName);
        //    CollectionAssert.Contains(result.ViewData.Keys, "Sucesso");
        //    CollectionAssert.DoesNotContain(result.ViewData.Keys, "Falha");
        //}

        //[Test]
        //public void CasoACamadaDeNegociosNaoSalveOLivroComSucesso_OControleDeveEnviarUmaMensagemDeFalhaEContinuarNaPaginaDeCadastro()
        //{
        //    var novoLivro = new Models.Product.Product
        //        {
        //        Name = "Torre Negra",
        //        Author = "Stephen King",
        //        Publishing = "Universal",
        //        Year = 1995,
        //        Category = Categories.LiteraturaEstrangeira,
        //        Stock = 5,
        //        Price = 150.0M,
        //        Photo = ""
        //    };

        //    var produto = new Product();

        //    produto.InjectFrom(novoLivro);

        //    _business.Setup(x => x.SalvarLivro(produto)).Returns(produto);

        //    _controller = new ProductController(_business.Object);

        //    var result = _controller.CadastrarLivro(novoLivro);

        //    StringAssert.AreEqualIgnoringCase("CadastrarLivro", result.ViewName);
        //    StringAssert.AreEqualIgnoringCase(novoLivro.Name, ((Models.Product.Product)result.Model).Name);
        //    CollectionAssert.DoesNotContain(result.ViewData.Keys, "Sucesso");
        //    CollectionAssert.Contains(result.ViewData.Keys, "Falha");
        //}
    }
}
