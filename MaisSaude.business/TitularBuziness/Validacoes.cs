using Dapper;
using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.TitularBuziness
{
    public partial class TitularBuziness : ITitularBuziness
    {
        public async Task<bool> VerificarEmailExistente(string email,string cpftitular)
        {
            
            var connection = _connectionDapper.connectionString();
            connection.Open();
            dynamic EmaiSl = connection.QueryFirstOrDefault("SELECT CPFTitular,Email FROM Titular WHERE Email = @email AND CPFTitular = @cpftitular", new { email, cpftitular });

            if (EmaiSl?.CPFTitular == cpftitular && EmaiSl?.Email == email)
            {
                return false;
            }

            dynamic Email = connection.QueryFirstOrDefault("SELECT Email FROM Titular WHERE Email = @email", new { email });
            if (Email?.Email != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
