using MaisSaude.Common;
using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.Login_home
{
    public interface ILogin
    {
        UsuarioAutenticado LoginHome(string usuario,string senha);

        void RestaurarSenha(RestaurarSenha redefinirSenha);
    }
}
