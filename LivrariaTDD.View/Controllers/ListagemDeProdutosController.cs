using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.View.Controllers;
using LivrariaTDD.Models;

namespace LivrariaTDD.Controllers
{
    public class ListagemDeProdutosController : Controller, IListagemDeProdutosController
    {
        private IListagemDeProdutosBusiness _business;

        public ListagemDeProdutosController(IListagemDeProdutosBusiness business)
        {
            this._business = business;
        }

        public ViewResult Index()
        {
            try
            {
                var listagemDeProdutos = _business.RecuperarTodosProdutos();
                ViewData["ListagemDeProdutos"] = listagemDeProdutos;
            }
            catch(Exception e)
            {
                ViewData["Erro"] = "Ocorreu um erro durante o processamento. Tente novamente mais tarde.";
            }
            
            return View("ListagemDeProdutos");
        }
    }
}