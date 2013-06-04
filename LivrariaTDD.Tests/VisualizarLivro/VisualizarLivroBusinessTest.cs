using System;
using System.Collections.Generic;
using LivrariaTDD.BRL;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace LivrariaTDD.MVCTests.VisualizarLivro
{
    [TestFixture]
    public class VisualizarLivroBusinessTest
    {
        private IVisualizarLivroBusiness _business;
        private Mock<IProdutoRepository> _repository;
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



            _repository = new Mock<IProdutoRepository>();
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(1)).Returns(_livroTDD);
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(2)).Returns(_livroRomance);
            _repository.Setup(x => x.RecuperarInformacoesDoLivro(3)).Returns(_livroFiccao);
            _business = new VisualizarLivroBusiness(_repository.Object);
        }
        
        #region US3

        [Test]
        public void AoAcessarACamadaDeNegociosParaVisualizarUmLivro_OLivroDeveVirDaCamadaDeAcessoADados()
        {
            var id = 1;

            var result = _business.RecuperarInformacoesDoLivro(id);

            _repository.Verify(x => x.RecuperarInformacoesDoLivro(id), Times.AtLeastOnce());
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IProduto>(result);
        }

        [Test]
        public void AoAcessarACamadaDeNegociosDaPaginaDeListagemEOcorreUmaExcecao_AExcecaoDeveSerLancadaParaCamadaSuperior()
        {
            var id = 1;

            var repository = new Mock<IProdutoRepository>();

            repository.Setup(x => x.RecuperarInformacoesDoLivro(id)).Throws<Exception>();

            var business = new VisualizarLivroBusiness(repository.Object);

            Assert.Throws<Exception>(() => business.RecuperarInformacoesDoLivro(id));
        }

        #endregion
    }
}