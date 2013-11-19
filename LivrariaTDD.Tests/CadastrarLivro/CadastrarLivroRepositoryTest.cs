using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.CadastrarLivro
{
    [TestFixture]
    public class CadastrarLivroRepositoryTest
    {
        private IProductRepository _repository;

        [SetUp]
        public void SetUp()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach(var item in db.Products.ToList())
                {
                    db.Products.Remove(item);
                }

                db.SaveChanges();
            }
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmLivro_DeveSalvarOLivroERetornarVerdadeiro()
        {
            var novoLivro = new Product
            {
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = ""
            };

            var auxContext = new LivrariaTDDContext();

            var mockContext = new LivrariaTDDContext();

            _repository = new ProdutoRepository(mockContext);

            var countAntes = auxContext.Products.Count();

            var result = _repository.SalvarLivro(novoLivro);

            var countDepois = auxContext.Products.Count();            

            Assert.NotNull(result);
            Assert.AreEqual(0,countAntes);
            Assert.AreEqual(1,countDepois);
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmLivroEReceberUmErro_NaoDeveSalvarOLivroERetonarFalso()
        {
            var novoLivro = new Product
            {
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = ""
            };

            var auxContext = new LivrariaTDDContext();

            var mockContext = new LivrariaTDDContext("server=./SQLSeverS/tring Errada");

            _repository = new ProdutoRepository(mockContext);

            var countAntes = auxContext.Products.Count();

            var result = _repository.SalvarLivro(novoLivro);

            var countDepois = auxContext.Products.Count();

            Assert.Null(result);
            Assert.AreEqual(countAntes, 0);
            Assert.AreEqual(countDepois, 0);
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmLivro_OLivroDeveSerCadastradoComStatus1Ativo()
        {
            var novoLivro = new Product
            {
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = ""
            };

            var mockContext = new LivrariaTDDContext();

            _repository = new ProdutoRepository(mockContext);

            _repository.SalvarLivro(novoLivro);

            var result = mockContext.Products.FirstOrDefault().Status;

            Assert.AreEqual(ProductStatus.Active, result);
        }
    }
}
