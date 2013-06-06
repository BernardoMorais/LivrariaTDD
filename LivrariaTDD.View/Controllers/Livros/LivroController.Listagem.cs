using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.Helpers;
using LivrariaTDD.Models;
using Omu.ValueInjecter;
using System.Linq;

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

        public ViewResult Index(bool logado, string tipoUsuario, string erroLogin)
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

        public ViewResult PesquisaProduto(string nome, string categoria)
        {
            Helpers.CarregarDadosUsuario(ViewData);

            if (ViewData.ContainsKey("ListagemDeProdutos"))
            {
                if (ViewData["ListagemDeProdutos"] == null)
                {
                    ViewData["ListagemDeProdutos"] = CarregaListaDeProdutos();
                }
            }
            else
                ViewData.Add("ListagemDeProdutos", CarregaListaDeProdutos());

            var lista = (List<ProdutoModel>)ViewData["ListagemDeProdutos"];

            if(!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(categoria))
                ViewData["ListagemDeProdutos"] = lista.Where(x => (String.IsNullOrEmpty(nome) && String.IsNullOrEmpty(categoria)) || ((!String.IsNullOrEmpty(nome) && x.Nome.Contains(nome)) && (!String.IsNullOrEmpty(categoria) && x.Categoria.Contains(categoria)))).ToList();
            else
                ViewData["ListagemDeProdutos"] = lista.Where(x => (String.IsNullOrEmpty(nome) && String.IsNullOrEmpty(categoria)) || ((!String.IsNullOrEmpty(nome) && x.Nome.Contains(nome)) || (!String.IsNullOrEmpty(categoria) && x.Categoria.Contains(categoria)))).ToList();

            return View("ListagemDeProdutos");
        }
    }
}