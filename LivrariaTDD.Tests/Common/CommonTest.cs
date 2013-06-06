using System.Web.Mvc;
using LivrariaTDD.Controllers;
using LivrariaTDD.Controllers.Livros;
using LivrariaTDD.Controllers.Usuario;
using LivrariaTDD.Infrastructure.BRL.Livro;
using LivrariaTDD.Infrastructure.BRL.Usuario;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.Common
{
    [TestFixture]
    public class CommonTest
    {
        [Test]
        public void TodosControlesDevemHerdarDeController()
        {
            /* Neste caso não foi possível testas utlizando mocks, pois a interface não pode herdar da classe "Controller" */
            var homeController = new HomeController();
            var livroController = new LivroController(new Mock<ILivroBusiness>().Object);
            var usuarioController = new UsuarioController(new Mock<IUsuarioBusiness>().Object);

            Assert.IsInstanceOf(typeof(Controller), homeController);
            Assert.IsInstanceOf(typeof(Controller), livroController);
            Assert.IsInstanceOf(typeof(Controller), usuarioController);
        }
    }
}