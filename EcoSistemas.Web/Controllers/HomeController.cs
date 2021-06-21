using EcoSistemas.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EcoSistemas.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] IConfiguration config)
        {
            var token = GetToken(config);
            Token viewDataVariavel = ViewData["Token"] as Token;
            var paciente = GetPaciente(config, viewDataVariavel);
            return View();
        }

        public IActionResult GetToken([FromServices] IConfiguration config)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string baseURL = config.GetSection("EcoSistemas_API:BaseURL").Value;

                HttpResponseMessage response = client.GetAsync(baseURL + "api/Desafio/GetToken").Result;

                response.EnsureSuccessStatusCode();
                string conteudo = response.Content.ReadAsStringAsync().Result;

                dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                Token token = new Token();
                token.data = resultado.data;
                token.mensagem = resultado.mensagem;
                token.proximaEtapa = resultado.proximaEtapa;
                token.statusCode = resultado.statusCode;

                ViewData["Token"] = token;


                return View(token);

            }
        }
        public IActionResult GetPaciente([FromServices] IConfiguration config,Token token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string baseURL = config.GetSection("EcoSistemas_API:BaseURL").Value;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.data);
                HttpResponseMessage response = client.GetAsync(baseURL + "api/Desafio/GetPaciente").Result;
                response.EnsureSuccessStatusCode();

                string conteudo = response.Content.ReadAsStringAsync().Result;

                dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                dynamic dados = resultado.data;

                Paciente paciente = new Paciente();
                paciente.usu_nome = dados.usu_nome;
                paciente.email = dados.email;
                paciente.telefone = dados.telefone;
                paciente.sexo = dados.sexo;
                paciente.bairro = dados.bairro;

                ViewData["Paciente"] = paciente;

                return View(paciente);

            }
        }
    }
}
