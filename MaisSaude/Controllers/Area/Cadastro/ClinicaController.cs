using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Common;
using MaisSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace MaisSaude.Controllers.Area.Cadastro
{
    public class ClinicaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public ClinicaController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
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

                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<IEnumerable<Clinica>>(await r.Content.ReadAsStringAsync()));
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

        // GET: CadastroClinicaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/DetalhesClinica?Id={id}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<Clinica>(await r.Content.ReadAsStringAsync()));
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

        // GET: CadastroClinicaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroClinicaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CadastroClinicaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/DetalhesClinica?Id={id}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<Clinica>(await r.Content.ReadAsStringAsync()));
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

        // POST: CadastroClinicaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Clinica clinica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Dependente/UpdateDependente", clinica);

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

        // GET: CadastroClinicaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroClinicaController/Delete/5
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
