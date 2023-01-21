using MaisSaude.Common;
using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area.Cadastro
{
    [Authorize(Roles = "administrador")]
    public class DependenteController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public DependenteController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = httpClient.CreateClient();
            _dadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {

            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Dependente");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<IEnumerable<Dependente>>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar listar dependentes");

                }
            }
            catch (Exception)
            {

                throw;
            }

        
        }
        private async Task<List<SelectListItem>> CaregarTitular()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ListaTitulares");

            if (response.IsSuccessStatusCode)
            {
                List<Titular> veiculos = JsonConvert.DeserializeObject<List<Titular>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in veiculos)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.CPFTitular,
                        Text = linha.Nome +" - "+ linha.CPFTitular,
                        Selected = false,
                    });
                }
                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

       public ActionResult Details(int id)
        {
            return View();
        }


        public async Task<ActionResult> Create()
        {
            ViewBag.ComboTitular = await CaregarTitular();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Dependente dependente)
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Dependente/InserirDependente", dependente);
                if (r.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                }
                else
                {
                    ViewBag.ComboTitular = await CaregarTitular();
                    throw new Exception("Erro ao tentar listar dependentes");
                };
            }
            catch
            {
                ViewBag.ComboTitular = await CaregarTitular();
                return View();
            }
        }

        public async Task<ActionResult> Edit(string CPF)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Dependente/DetalhesDependente?CPF={CPF}");
                if (r.IsSuccessStatusCode)
                {
                    var teste = JsonConvert.DeserializeObject<IEnumerable<Dependente>>(await r.Content.ReadAsStringAsync());                 
                    return View(teste.FirstOrDefault());
                }
                else
                {
                    throw new Exception("Erro ao tentar listar dependentes");
                };
            }
            catch
            {
                return View();
            }
        }

        // POST: CadastroDependenteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Dependente dependente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Dependente/UpdateDependente", dependente);

                    if (r.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });
                    else
                        throw new Exception("Erro ao tentar Editar um títular!");

                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroDependenteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroDependenteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
