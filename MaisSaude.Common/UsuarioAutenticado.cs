﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Common
{
    public class UsuarioAutenticado
    {

        public bool Titular { get; set; }
        public bool Dependente { get; set; }
        public bool Clinica { get; set; }

        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string TipoPermissao { get; set; }
    }
}