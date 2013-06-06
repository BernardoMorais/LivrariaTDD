using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Usuario;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models;

namespace LivrariaTDD.Controllers.Usuario
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioBusiness _business;        

        public UsuarioController(IUsuarioBusiness business)
        {
            _business = business;
        }

        public RedirectToRouteResult Entrar(UsuarioModel usuario)
        {
            if (_business.ValidarUsuario(usuario.Email, Helpers.ConvertToSHA1(usuario.Senha)))
            {
                return RedirectToAction("Index", "ListagemDeProdutos", new { logado = true, tipoUsuario = _business.VerificarTipoUsuario(usuario.Email), erroLogin = "" });
            }
            return RedirectToAction("Index", "ListagemDeProdutos", new { logado = false, tipoUsuario = "", erroLogin = "Dados do usuário inválidos." });
        }

        public RedirectToRouteResult Sair()
        {
            return RedirectToAction("Index", "ListagemDeProdutos", new { logado = false, tipoUsuario = "", erroLogin = "" });
        }
    }
}