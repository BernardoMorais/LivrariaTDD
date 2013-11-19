using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaTDD.BRL.Pedido;
using LivrariaTDD.Infrastructure.BRL.Pedido;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Infrastructure.Models;
using NUnit.Framework;
using Moq;

namespace LivrariaTDD.MVCTests.CarrinhoDeCompras
{
    [TestFixture]
    public class CarrinhoDeComprasBusinessTest
    {
        private IPedidoBusiness _business;
        private Mock<IOrderRepository> _repository;
        private int _userId;
        private List<int> _listaIdProdutos;
        private int _idFormaDePagamento;
        private Order _pedido;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Mock<IOrderRepository>();
            _business = new PedidoBusiness(_repository.Object);
            _userId = 1;
            _listaIdProdutos = new List<int> { 1, 2 };
            _idFormaDePagamento = 1;
            _pedido = new Order {OrderValue = 100.0M, FreightValue = 10.0M, TotalValue = 110.0M};
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitaFechamentoDeUmPedido_ACamadaDeNegociosDeveAcessarACamadaDeAcessoADadosParaSalvarOPedido()
        {

            _repository.Setup(x => x.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento)).Returns(true);

            _business.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento);

            _repository.Verify(x => x.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento), Times.AtLeastOnce());
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitaFechamentoDeUmPedidoEOMesmoESalvoComSucesso_DeveRetornarVedadeiroParaQuemAChamou()
        {
            _repository.Setup(x => x.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento)).Returns(true);

            var result = _business.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento);

            Assert.True(result);
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitaFechamentoDeUmPedidoEOcorreUmaFalha_DeveRetornarFalsoParaQuemAChamou()
        {
            _repository.Setup(x => x.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento)).Returns(false);

            var result = _business.SalvarPedido(_pedido, _userId, _listaIdProdutos, _idFormaDePagamento);

            Assert.False(result);
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitarCalculoDeFrete_OSistemaDeveVerificarSeOCepEValido()
        {
            const int cepInvalido = 00000000;
            const int cepValido = 30624130;

            Assert.False(Helpers.ValidarCep(cepInvalido));
            Assert.True(Helpers.ValidarCep(cepValido));
        }

        [Test]
        public void QuandoAlgumPersonagemSolicitarCalculoDeFrete_OSistemaDeveRetornarOValorDoFrete()
        {
            //O valor do frete é uma simulçao, levanto em consideração somente a quantidade de produtos vezes R$ 5,00
            const decimal freteUmLivro = 5.0M;
            const decimal freteDoisLivros = 10.0M;
            const decimal freteCemLivros = 500.0M;

            Assert.AreEqual(freteUmLivro, _business.CalcularFrete(1));
            Assert.AreEqual(freteDoisLivros, _business.CalcularFrete(2));
            Assert.AreEqual(freteCemLivros, _business.CalcularFrete(100));
        }
    }
}
