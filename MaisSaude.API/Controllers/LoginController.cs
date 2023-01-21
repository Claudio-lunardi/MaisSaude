
using MaisSaude.API.Login;
using MaisSaude.Business.Login_home;
using MaisSaude.Common;
using MaisSaude.Common.Login.ModelLogin;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;
        public LoginController(ILogin login)
        {
            _login = login;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRespostaModel>> Login([FromBody] LoginRequisicaoModel loginRequisicaoModel)
        {
            return Ok(await new LoginServico().Login(loginRequisicaoModel));
        }


        [HttpGet("LoginHome")]      
        public UsuarioAutenticado LoginHOME([FromQuery] string Usuario, string Senha)
        {
            return _login.LoginHome(Usuario, Senha);
        }




    }
}
