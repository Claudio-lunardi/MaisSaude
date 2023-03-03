﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Common;
using Microsoft.Extensions.Options;
using MaisSaude.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using MaisSaude.Business.Login_home.Models;
using System.Net.Http.Headers;

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
                        if (re.Dependente || re.Titular || re.Clinica)
                        {

                            var identity = new ClaimsIdentity(new[]
                            {
                        new Claim(ClaimTypes.NameIdentifier, re.Usuario),
                        new Claim(ClaimTypes.Name, re.Nome),
                        new Claim(ClaimTypes.Role, re.TipoPermissao),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                            return Json("OK");
                        }
                        else
                        {
                            TempData["erroLogin"] = "Usuário ou Senha inválido!";
                            return Json("Usuário ou Senha inválido!");
                        }
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
    }
}
