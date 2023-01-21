using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Common.Login.ModelLogin
{
    public class LoginRequisicaoModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
