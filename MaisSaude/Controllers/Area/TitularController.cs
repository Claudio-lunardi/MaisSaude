using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area
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

        #region INDEX
        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {
            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Titular/ListaTitulares");

                if (r.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<List<Titular>>(await r.Content.ReadAsStringAsync()));
                else
                    throw new Exception("Erro ao carregar Lista de títulares");

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GET
        public async Task<ActionResult> Details(string CPF)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Titular/ObterUmTitular?CPF={CPF}");

                if (r.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<Titular>(await r.Content.ReadAsStringAsync()));
                else
                    throw new Exception("Erro ao tentar mostrar um Títular!");

            }
            catch (Exception)
            {
                throw;
            }
        }



        #endregion

        #region POST
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Titular titular)
        {
            try
            {
                //if (ModelState.IsValid)
                //{      
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Titular", titular);

                if (r.IsSuccessStatusCode)
                {
                    if (await r.Content.ReadAsStringAsync() == "O e-mail informado ja é existente!")
                    {
                        TempData["warning"] = r.ReasonPhrase;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                    }
                }
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


        #endregion

        #region EDIT
        public async Task<ActionResult> Edit(int ID)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Titular/ObterUmTitular?ID={ID}");

                if (r.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<Titular>(await r.Content.ReadAsStringAsync()));
                else
                    throw new Exception("Erro ao tentar mostrar um Títular!");

            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Titular titular)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Titular/UpdateTitular", titular);

                    if (r.IsSuccessStatusCode)
                    {
                        if (await r.Content.ReadAsStringAsync() == "O e-mail informado ja é existente!")
                        {
                            TempData["warning"] = await r.Content.ReadAsStringAsync();
                            return View();
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });
                        }
                    }
                    else
                        throw new Exception("Erro ao tentar incluir um títular!");
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

        #endregion

        public async Task<ActionResult> api(string cpf)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Titular/ListaDependente?CPFTitular={cpf}");

            if (r.IsSuccessStatusCode)
                return Json(JsonConvert.DeserializeObject<IEnumerable<Dependente>>(await r.Content.ReadAsStringAsync()));
            else
                throw new Exception("Erro ao tentar mostrar um Títular!");

        }

    }
}
