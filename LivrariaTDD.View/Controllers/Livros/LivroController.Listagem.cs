using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Livros
{
    public partial class LivroController
    {
        public ViewResult Index()
        {
            try
            {
                Helpers.CarregarDadosUsuario(ViewData);

                if (ViewData.ContainsKey("ListagemDeProdutos"))
                    ViewData["ListagemDeProdutos"] = CarregaListaDeProdutos();
                else
                    ViewData.Add("ListagemDeProdutos", CarregaListaDeProdutos());
            }
            catch (Exception)
            {
                if (ViewData.ContainsKey("Erro"))
                    ViewData["Erro"] = "Ocorreu um erro durante o processamento. Tente novamente mais tarde.";
                else
                    ViewData.Add("Erro", "Ocorreu um erro durante o processamento. Tente novamente mais tarde.");
            }
            
            return View("ListagemDeProdutos");
        }

        public ViewResult Entrar(bool logado, string tipoUsuario, string erroLogin)
        {
            try
            {
                Helpers.CarregarDadosUsuario(ViewData, logado, tipoUsuario, erroLogin);

                if (ViewData.ContainsKey("ListagemDeProdutos"))
                    ViewData["ListagemDeProdutos"] = CarregaListaDeProdutos();
                else
                    ViewData.Add("ListagemDeProdutos", CarregaListaDeProdutos());
            }
            catch (Exception)
            {
                if (ViewData.ContainsKey("Erro"))
                    ViewData["Erro"] = "Ocorreu um erro durante o processamento. Tente novamente mais tarde.";
                else
                    ViewData.Add("Erro", "Ocorreu um erro durante o processamento. Tente novamente mais tarde.");
            }

            return View("ListagemDeProdutos");
        }

        public List<Models.Product.Product> CarregaListaDeProdutos()
        {
            var listagemDeProdutos = _business.GetAll();
            var novaListagemDeProdutos = new List<Models.Product.Product>();

            foreach (var produto in listagemDeProdutos)
            {
                var novoProduto = new Models.Product.Product();
                novoProduto.InjectFrom(produto);
                novaListagemDeProdutos.Add(novoProduto);
            }

            return novaListagemDeProdutos;
        }
    }
}