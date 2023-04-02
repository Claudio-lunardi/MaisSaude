using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MaisSaude.Controllers.Area.Login
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public LoginController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = httpClient.CreateClient();
            _dadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Entrar(string usuario, string senha)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
            HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Login/LoginHome?Usuario={usuario}&Senha={senha}");
            if (r.IsSuccessStatusCode)
            {
                try
                {
                    var re = JsonConvert.DeserializeObject<UsuarioAutenticado>(await r.Content.ReadAsStringAsync());
                    if (re != null)
                    {
                        var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, re.Usuario),
                        new Claim(ClaimTypes.Name, re.Nome),
                        new Claim(ClaimTypes.Email, re.Email),
                        new Claim(ClaimTypes.Role, re.TipoPermissao),
                        new Claim("ID", re.ID)
                             }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return Json("OK");
                    }
                    else
                    {
                        TempData["erroLogin"] = "Usuário Não existe";
                        return Json("Usuário Não existe");
                    }
                }
                catch (Exception)
                {
                    TempData["erroLogin"] = "Usuário ou Senha inválido!";
                    return Json("Usuário ou Senha inválido!");
                }
            }
            else
            {
                return Json("Erro");
            }
        }





        public async Task<ActionResult> PrimeiroAcesso()
        {
            Titular titular = new Titular()
            {
                DataInclusao = DateTime.Now
            };

            return View(titular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PrimeiroAcesso(Titular titular)
        {
            try
            {
                titular.DataInclusao = DateTime.Now;
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Titular/InsertTitular", titular);

                    if (r.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
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
    }
}
