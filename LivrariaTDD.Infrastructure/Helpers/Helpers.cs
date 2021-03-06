﻿using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace LivrariaTDD.Infrastructure.Helpers
{
    public static class Helpers
    {
        public static string ConvertToSHA1(string text)
        {
            try
            {
                var encoder = new System.Text.ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(text);
                var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(
                    cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

                return hash.ToLower();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void CarregarDadosUsuario(ViewDataDictionary viewData)
        {
            if (viewData.ContainsKey("logado"))
                viewData["logado"] = viewData["logado"];
            else
                viewData.Add("logado", false);

            if (viewData.ContainsKey("tipoUsuario"))
                viewData["tipoUsuario"] = viewData["tipoUsuario"];
            else
                viewData.Add("tipoUsuario", "");

            if (viewData.ContainsKey("erroLogin"))
                viewData["erroLogin"] = viewData["erroLogin"];
            else
                viewData.Add("erroLogin", "");
        }

        public static void CarregarDadosUsuario(ViewDataDictionary viewData, bool logado, string tipoUsuario, string erroLogin)
        {
            if (viewData.ContainsKey("logado"))
                viewData["logado"] = viewData["logado"];
            else
                viewData.Add("logado", logado);

            if (viewData.ContainsKey("tipoUsuario"))
                viewData["tipoUsuario"] = viewData["tipoUsuario"];
            else
                viewData.Add("tipoUsuario", tipoUsuario);

            if (viewData.ContainsKey("erroLogin"))
                viewData["erroLogin"] = viewData["erroLogin"];
            else
                viewData.Add("erroLogin", erroLogin);
        }

        //Método que valida o Cep
        public static bool ValidarCep(int cepInt)
        {
            var cep = cepInt.ToString(CultureInfo.InvariantCulture);
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
                //txt.Text = cep;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }
    }
}
