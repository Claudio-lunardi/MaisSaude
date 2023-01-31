using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area.Cadastro
{
    [Authorize(Roles = "clinica")]
    public class TitularController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public TitularController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
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
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ListaTitulares");

                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<List<Titular>>(await r.Content.ReadAsStringAsync()));
                }
                else
                {

                    throw new Exception("Erro ao carregar Lista de títulares");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: CadastroClienteController/Details/5
        public async Task<ActionResult> Details(string CPF)
        {
            try
            {
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterUmTitular?CPF={CPF}");

                if (r.IsSuccessStatusCode)
                {
                    var F = await r.Content.ReadAsStringAsync();
                    var teste = JsonConvert.DeserializeObject<Titular>(F);

                    return View(teste);
                }
                else
                {
                    throw new Exception("Erro ao tentar mostrar um Títular!");
                }


            }
            catch (Exception)
            {

                throw;
            }


        }

        // GET: CadastroClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Titular titular)
        {
            try
            {
                //titular.DataInclusao = DateTime.Now;
                //if (ModelState.IsValid)
                //{      
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente", titular);

                if (r.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                else
                    throw new Exception("Erro ao tentar incluir um títular!");


                //}
                //else
                //{
                //    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                //    return View();
                //}
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroClienteController/Edit/5
        public async Task<ActionResult> Edit(string CPF)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterUmTitular?CPF={CPF}");

                if (r.IsSuccessStatusCode)
                {
                    var F = await r.Content.ReadAsStringAsync();
                    var teste = JsonConvert.DeserializeObject<Titular>(F);

                    return View(teste);
                }
                else
                {
                    throw new Exception("Erro ao tentar mostrar um Títular!");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: CadastroClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Titular titular)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/UpdateTitular", titular);

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

        // GET: CadastroClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroClienteController/Delete/5
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



        public async Task<ActionResult> api(string cpf)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ListaDependente?CPFTitular={cpf}");

            if (r.IsSuccessStatusCode)
            {

                var teste = JsonConvert.DeserializeObject<IEnumerable<Dependente>>(await r.Content.ReadAsStringAsync());

                return Json(teste);
            }
            else
            {
                throw new Exception("Erro ao tentar mostrar um Títular!");
            }
        }

    }
}
