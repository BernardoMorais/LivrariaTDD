using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.ExcluirLivro
{
    [TestFixture]
    public class ExcluirLivroRepositoryTest
    {
        private IProductRepository _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            using (var db = new LivrariaTDDContext())
            {
                foreach (var item in db.Products.ToList())
                {
                    db.Products.Remove(item);
                }

                db.SaveChanges();
            }
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaExcluirUmLivro_OLivroDeveSerExcluidoEOMetodoDeveRetornarVerdadeiro()
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
                Photo = "",
                Status = ProductStatus.Active
            };

            using (var livrariaContext = new LivrariaTDDContext())
            {
                novoLivro = livrariaContext.Products.Add(novoLivro);
                livrariaContext.SaveChanges();
            }


            using(var auxContext = new LivrariaTDDContext())
            {
                var firstOrDefault = auxContext.Products.FirstOrDefault(x => x.ProductId == novoLivro.ProductId);
                if (firstOrDefault != null)
                {
                    var statusAntes = firstOrDefault.Status;
                    Assert.AreEqual(ProductStatus.Active, statusAntes);
                }
            }

            using (var livrariaContext = new LivrariaTDDContext())
            {
                _repository = new ProdutoRepository(livrariaContext);

                var result = _repository.ExcluirLivro(novoLivro.ProductId);

                Assert.True(result);
            }

            using(var auxContext = new LivrariaTDDContext())
            {
                var firstOrDefault = auxContext.Products.FirstOrDefault(x => x.ProductId == novoLivro.ProductId);
                if (firstOrDefault != null)
                {
                    var statusDepois = firstOrDefault.Status;
                    Assert.AreEqual(ProductStatus.Inative, statusDepois);
                }
            }
        }

        [Test]
        public void AoAcessarACamadaDeAcessoADadosParaExcluirUmLivroNaoExistente_OMetodoDeveRetornarFalso()
        {
            const int idFalso = -10;

            var novoLivro = new Product
            {
                Name = "Torre Negra",
                Author = "Stephen King",
                Publishing = "Universal",
                Year = 1995,
                Category = Categories.LiteraturaEstrangeira,
                Stock = 5,
                Price = 150.0M,
                Photo = "",
                Status = ProductStatus.Active
            };

            using (var livrariaContext = new LivrariaTDDContext())
            {
                novoLivro = livrariaContext.Products.Add(novoLivro);
                livrariaContext.SaveChanges();
            }


            using (var auxContext = new LivrariaTDDContext())
            {
                var firstOrDefault = auxContext.Products.FirstOrDefault(x => x.ProductId == novoLivro.ProductId);
                if (firstOrDefault != null)
                {
                    var statusAntes = firstOrDefault.Status;
                    Assert.AreEqual(ProductStatus.Active, statusAntes);
                }
            }

            using (var livrariaContext = new LivrariaTDDContext())
            {
                _repository = new ProdutoRepository(livrariaContext);

                var result = _repository.ExcluirLivro(idFalso);

                Assert.False(result);
            }

            using (var auxContext = new LivrariaTDDContext())
            {
                var firstOrDefault = auxContext.Products.FirstOrDefault(x => x.ProductId == novoLivro.ProductId);
                if (firstOrDefault != null)
                {
                    var statusDepois = firstOrDefault.Status;
                    Assert.AreEqual(ProductStatus.Active, statusDepois);
                }
            }
        }
    }
}
