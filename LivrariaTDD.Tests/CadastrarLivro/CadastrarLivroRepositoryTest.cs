using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaTDD.DAL.Models;
using LivrariaTDD.DAL.Repositories;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.CadastrarLivro
{
    [TestFixture]
    public class CadastrarLivroRepositoryTest
    {
        private IProdutoRepository _repository;
        private EnumerableQuery<IProduto> _listaDeProdutos;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _listaDeProdutos = new EnumerableQuery<IProduto>(new[]
                {
                    new Produto
                        {
                            Nome = "TDD desenvolvimento guiado por testes",
                            Autor = "Kent Beck",
                            Editora = "Bookman",
                            Ano = 2010,
                            Categoria = "Engenharia de Software",
                            Estoque = 0,
                            Preco = 50.0M,
                            Foto = ""
                        }
                });
        }

        [Test]
        [Ignore]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmLivro_OsProdutosDevemVirDaDoFrameworkDeORM()
        {
            var novoLivro = new Produto
            {
                Nome = "Torre Negra",
                Autor = "Stephen King",
                Editora = "Universal",
                Ano = 1995,
                Categoria = "Ficção",
                Estoque = 5,
                Preco = 150.0M,
                Foto = ""
            };

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            _repository.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            mockContext.Verify(x => x.SaveChanges(), Times.AtLeastOnce());
        }

        [Test]
        [Ignore]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmLivro_DeveSalvarOLivroERetornarVerdadeiro()
        {
            var novoLivro = new Produto
            {
                Nome = "Torre Negra",
                Autor = "Stephen King",
                Editora = "Universal",
                Ano = 1995,
                Categoria = "Ficção",
                Estoque = 5,
                Preco = 150.0M,
                Foto = ""
            };

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);

            _repository = new ProdutoRepository(mockContext.Object);

            var countAntes = mockContext.Object.Produtos.Count();            

            var result = _repository.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            var countDepois = mockContext.Object.Produtos.Count();            

            Assert.True(result);
            Assert.AreEqual(countAntes,1);
            Assert.AreEqual(countDepois, 2);
        }

        [Test]
        [Ignore]
        public void AoAcessarACamadaDeAcessoADadosParaSalvarUmLivroEReceberUmErro_DeveNaoDeveSalvarOLivroERetonarFalso()
        {
            var novoLivro = new Produto
            {
                Nome = "Torre Negra",
                Autor = "Stephen King",
                Editora = "Universal",
                Ano = 1995,
                Categoria = "Ficção",
                Estoque = 5,
                Preco = 150.0M,
                Foto = ""
            };

            var mockContext = new Mock<ILivrariaTDDContext>();

            mockContext.Setup(x => x.Produtos).Returns(_listaDeProdutos);
            mockContext.Setup(x => x.SaveChanges()).Throws<Exception>();

            _repository = new ProdutoRepository(mockContext.Object);

            var countAntes = mockContext.Object.Produtos.Count();

            var result = _repository.SalvarLivro(novoLivro.Nome, novoLivro.Autor, novoLivro.Editora, novoLivro.Ano, novoLivro.Categoria, novoLivro.Estoque, novoLivro.Preco, novoLivro.Foto);

            var countDepois = mockContext.Object.Produtos.Count();

            Assert.False(result);
            Assert.AreEqual(countAntes, 1);
            Assert.AreEqual(countDepois, 1);
        }
    }
}
