﻿namespace MaisSaude.Common.Login.ModelLogin
{
    public class LoginRespostaModel
    {
        public string Usuario { get; set; }
        public bool Autenticado { get; set; }
        public string Token { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
