using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Pedido;

namespace LivrariaTDD.Controllers.Pedido
{
    public class CarrinhoDeComprasController : Controller
    {
        private IPedidoBusiness _business;

        public CarrinhoDeComprasController(IPedidoBusiness pedidoBusiness)
        {
            _business = pedidoBusiness;
        }

        public JsonResult AdicionarProdutoAoCarrinho(int idProduto)
        {
            Dictionary<int, int> carrinhoDeCompras;

            if(Session["Carrinho"] != null)
            {
                carrinhoDeCompras = (Dictionary<int, int>) Session["Carrinho"];
            }
            else
            {
                carrinhoDeCompras = new Dictionary<int, int>();
            }

            if (carrinhoDeCompras.Keys.Contains(idProduto))
            {
                carrinhoDeCompras[idProduto]++;
            }
            else
            {
                carrinhoDeCompras.Add(idProduto, 1);
            }

            Session["Carrinho"] = carrinhoDeCompras;

            return Json(true);
        }

        public ActionResult Index()
        {
            throw new System.NotImplementedException();
        }
    }
}