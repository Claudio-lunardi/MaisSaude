using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MaisSaude.Controllers.Area.Home
{
    [Authorize(Roles = "clinica, dependente, titular")]

    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public HomeController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = httpClient.CreateClient();
            _dadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        public IActionResult Index(string mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;
            return View();
        }

        public ActionResult EditarDadosCadastrais()
        {
            var TypeClient = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            var ID = User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault();

            if ("titular" == TypeClient)
            {
                return RedirectToAction("EditTitular", "Home", new { ID = ID });
            }
            else if ("dependente" == TypeClient)
            {
                return RedirectToAction("EditDependente", "Home", new { ID = ID });
            }
            else if ("clinica" == TypeClient)
            {
                return RedirectToAction("EditClinica", "Home", new { ID = ID });

            }
            else
            {
                return View();
            }



        }





        #region Clinica

        public async Task<ActionResult> EditClinica(int ID)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/DetalhesClinica?Id={ID}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<Clinica>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar editar!");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditClinica(Clinica clinica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Clinica", clinica);

                    if (r.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });
                    else
                        throw new Exception("Erro ao tentar editar!");

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

        #region Dependente
        public async Task<ActionResult> EditDependente(int ID)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Dependente/DetalhesDependente?ID={ID}");

                if (r.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<IEnumerable<Dependente>>(await r.Content.ReadAsStringAsync()).FirstOrDefault());
                else
                    throw new Exception("Erro ao tentar editar!");

            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDependente(Dependente dependente)
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
                        throw new Exception("Erro ao tentar editar!");

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

        #region Titular
        public async Task<ActionResult> EditTitular(int ID)
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
        public async Task<ActionResult> EditTitular(Titular titular)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Titular/UpdateTitular", titular);

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
        #endregion



        public async Task<ActionResult> api(string CPF)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Titular/ListaDependente?CPF={CPF}");

            if (r.IsSuccessStatusCode)
                return Json(JsonConvert.DeserializeObject<IEnumerable<Dependente>>(await r.Content.ReadAsStringAsync()));
            else
                throw new Exception("Erro ao tentar mostrar um Títular!");

        }
































        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}