using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers
{
    public class ListagemDeProdutosController : Controller
    {
        private IListagemDeProdutosBusiness _business;

        public ListagemDeProdutosController(IListagemDeProdutosBusiness business)
        {
            _business = business;
        }

        public ViewResult Index()
        {
            try
            {
                if (ViewData.ContainsKey("ListagemDeProdutos"))
                    ViewData["ListagemDeProdutos"] = CarregaListaDeProdutos();
                else
                    ViewData.Add("ListagemDeProdutos", CarregaListaDeProdutos());
            }
            catch (Exception e)
            {
                if (ViewData.ContainsKey("Erro"))
                    ViewData["Erro"] = "Ocorreu um erro durante o processamento. Tente novamente mais tarde.";
                else
                    ViewData.Add("Erro", "Ocorreu um erro durante o processamento. Tente novamente mais tarde.");
            }
            
            return View("ListagemDeProdutos");
        }

        public ViewResult Index(bool logado, string tipoUsuario, string erroLogin)
        {
            try
            {
                if (ViewData.ContainsKey("logado"))
                    ViewData["logado"] = logado;
                else
                    ViewData.Add("logado", logado);

                if (ViewData.ContainsKey("tipoUsuario"))
                    ViewData["tipoUsuario"] = tipoUsuario;
                else
                    ViewData.Add("tipoUsuario", tipoUsuario);

                if (ViewData.ContainsKey("erroLogin"))
                    ViewData["erroLogin"] = erroLogin;
                else
                    ViewData.Add("erroLogin", erroLogin);

                if (ViewData.ContainsKey("ListagemDeProdutos"))
                    ViewData["ListagemDeProdutos"] = CarregaListaDeProdutos();
                else
                    ViewData.Add("ListagemDeProdutos", CarregaListaDeProdutos());
            }
            catch (Exception e)
            {
                if (ViewData.ContainsKey("Erro"))
                    ViewData["Erro"] = "Ocorreu um erro durante o processamento. Tente novamente mais tarde.";
                else
                    ViewData.Add("Erro", "Ocorreu um erro durante o processamento. Tente novamente mais tarde.");
            }

            return View("ListagemDeProdutos");
        }

        public List<ProdutoModel> CarregaListaDeProdutos()
        {
            var listagemDeProdutos = _business.RecuperarTodosProdutos();
            var novaListagemDeProdutos = new List<ProdutoModel>();

            foreach (var produto in listagemDeProdutos)
            {
                var novoProduto = new ProdutoModel();
                novoProduto.InjectFrom(produto);
                novaListagemDeProdutos.Add(novoProduto);
            }

            return novaListagemDeProdutos;
        }
    }
}