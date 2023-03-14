using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MaisSaude.Controllers.Area.Cadastro
{
    public class AgendamentoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public AgendamentoController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
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

                Agendamento agendamento = new Agendamento()
                {
                    UsuarioPaciente = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault()
                };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Agendamento?usuario={agendamento.UsuarioPaciente}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<List<Agendamento>>(await r.Content.ReadAsStringAsync()));
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


        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            Agendamento agendamento = new Agendamento()
            {
                UsuarioPaciente = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault(),
                Paciente = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault(),
                DataInclusao = DateTime.Now

            };
            return View(agendamento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Agendamento agendamento)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Agendamento/insert", agendamento);
                if (r.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { mensagem = "Registro salvo!", sucesso = true });
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit()
        {
            Agendamento agendamento = new Agendamento()
            {
                UsuarioPaciente = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault(),
                Paciente = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault(),
                DataInclusao = DateTime.Now

            };
            return View(agendamento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        public ActionResult Delete(int id)
        {
            return View();
        }

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
