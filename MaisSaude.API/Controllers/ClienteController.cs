using Dapper;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        [HttpPost]       
        public void ListaUmaCategoria()
        {
            const string connectionString = "Server=localhost,1433;Database=DBMaisSaude;User ID=sa;Password=senha@1234";

            var connection = new SqlConnection(connectionString);

            connection.Open();

            Titular titular = new Titular()
            {
                Nome = "sads",
                CPFTitular = "11111111111111"

            };

            string query = "INSERT INTO Titular( CPFTitular , Nome  , DataNascimento  , Telefone  , Celular  , Ativo  , DataCadastro  , Email  , Cep  , Cidade  , Estado , Complemento  , Numero  , Logradouro  , DataInclusao  , DataAlteracao VALUES(@CPFTitular , @Nome  , @DataNascimento  , @Telefone  , @Celular  , @Ativo  , @DataCadastro  , @Email  , @Cep  , @Cidade  , @Estado  , @Complemento  , @Numero  , @Logradouro  , @DataInclusao  , @DataAlteracao)";
            connection.ExecuteScalar(query, titular);


        }

    }
}
