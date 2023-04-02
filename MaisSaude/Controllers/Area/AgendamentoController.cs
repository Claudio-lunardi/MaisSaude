using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MaisSaude.Controllers.Area
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

        #region INDEX 
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
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Agendamento/GetAgendamentos?usuario={agendamento.UsuarioPaciente}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<List<Agendamento>>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar mostrar dependente");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region CREATE


        public async Task<ActionResult> Create()
        {
            Agendamento agendamento = new Agendamento()
            {
                UsuarioPaciente = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault(),
                Paciente = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault(),
                DataInclusao = DateTime.Now,
                Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault()
            };

            ViewBag.CaregarClinica = await CaregarClinica();
            ViewBag.CaregarEspecialidade = await CaregarEspecialidade();
            return View(agendamento);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Agendamento agendamento)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Agendamento/InsertAgendamento", agendamento);
                if (r.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { mensagem = "Registro salvo!", sucesso = true });
                }
                else
                {
                    ViewBag.CaregarClinica = await CaregarClinica();
                    ViewBag.CaregarEspecialidade = await CaregarEspecialidade();
                    return View();
                }
            }
            catch
            {
                ViewBag.CaregarClinica = await CaregarClinica();
                ViewBag.CaregarEspecialidade = await CaregarEspecialidade();
                return View();
            }
        }

        #endregion

        #region EDIT 


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


        #endregion

        #region VIEWBAGS
        private async Task<List<SelectListItem>> CaregarClinica()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/GetClinicas");

            if (response.IsSuccessStatusCode)
            {
                List<Clinica> clinicas = JsonConvert.DeserializeObject<List<Clinica>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in clinicas)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Nome,
                        Text = linha.Nome,
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

        private async Task<List<SelectListItem>> CaregarEspecialidade()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());

            HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Medico/GetMedicos");
            if (r.IsSuccessStatusCode)
            {
                List<Medico> medico = JsonConvert.DeserializeObject<List<Medico>>(await r.Content.ReadAsStringAsync());
                if (medico != null)
                {
                    foreach (var linha in medico)
                    {
                        lista.Add(new SelectListItem()
                        {
                            Value = linha.Especialidade,
                            Text = linha.Especialidade,
                            Selected = false,
                        });
                    }
                }
                return lista;
            }
            else
            {
                throw new Exception(r.ReasonPhrase);
            }
        }

        #endregion

    }
}
