using System.Web.Mvc;
using System.Web.Security;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models;

namespace LivrariaTDD.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioBusiness _business;        

        public UsuarioController(IUsuarioBusiness business)
        {
            _business = business;
        }

        public RedirectToRouteResult Entrar(UsuarioModel usuario)
        {
            if (_business.ValidarUsuario(usuario.Email, Helpers.ConvertoToSHA1(usuario.Senha)))
            {
                return RedirectToAction("Index", "ListagemDeProdutos", new { logado = true, tipoUsuario = "Funcionario", erroLogin = "" });
            }
            return RedirectToAction("Index", "ListagemDeProdutos", new { logado = false, tipoUsuario = "", erroLogin = "Dados do usuário inválidos." });
        }

        public RedirectToRouteResult Sair()
        {
            return RedirectToAction("Index", "ListagemDeProdutos", new { logado = false, tipoUsuario = "", erroLogin = "" });
        }
    }
}