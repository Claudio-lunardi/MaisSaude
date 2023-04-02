using System.Net.Mail;
using System.Net;
using System.Text;
using MaisSaude.Infra.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using MaisSaude.Models;

namespace MaisSaude.EmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _rabbitMQFactory;
        public Worker(ILogger<Worker> logger, RabbitMQFactory rabbitMQFactory)
        {
            _logger = logger;
            _rabbitMQFactory = rabbitMQFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult EmailAgendamento = canal.BasicGet("Email", false);
                BasicGetResult RestaurarSenha = canal.BasicGet("Restaurar senha", false);

                if (EmailAgendamento != null)
                {
                    var dados = JsonConvert.DeserializeObject<Agendamento>(Encoding.UTF8.GetString(EmailAgendamento.Body.ToArray()));
                    await EnviarEmailAgendamento(dados.Email, dados.Paciente, dados.DataConsulta);
                    canal.BasicAck(EmailAgendamento.DeliveryTag, true);
                }

                if (RestaurarSenha != null)
                {
                    var dados = JsonConvert.DeserializeObject<RestaurarSenha>(Encoding.UTF8.GetString(RestaurarSenha.Body.ToArray()));
                    await EnviarEmailRestaurarSenha(dados.Email, dados.Senha);
                    canal.BasicAck(RestaurarSenha.DeliveryTag, true);
                }


                await Task.Delay(5000, stoppingToken);
            }
        }



        #region GERAR EMAIL
        private async Task EnviarEmailAgendamento(string Email, string nome, DateTime dataConsulta)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("Turma1@devpratica.com.br");
            message.To.Add(Email);
            message.Subject = "Bem-Vindo!";
            message.IsBodyHtml = true;
            message.Body = "Ola" + nome + "Sua consulta foi marcado para o dia " + dataConsulta;

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("Turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(message);
        }
        private async Task EnviarEmailRestaurarSenha(string Email, string senha)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("Turma1@devpratica.com.br");
            message.To.Add(Email);
            message.Subject = "Bem-Vindo!";
            message.IsBodyHtml = true;
            message.Body = "sua senha e: " + senha;

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("Turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(message);
        }

        #endregion



    }
}