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
                BasicGetResult retorno = canal.BasicGet("Email", false);

                if (retorno != null)
                {
                    var dados = JsonConvert.DeserializeObject<Agendamento>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    await EnviarEmail(dados.Email, dados.Paciente);
                    canal.BasicAck(retorno.DeliveryTag, true);
                }
         

                await Task.Delay(5000, stoppingToken);
            }
        }



        #region GERAR EMAIL
        private async Task EnviarEmail(string Email, string nome)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("Turma1@devpratica.com.br");
            message.To.Add(Email);
            message.Subject = "Bem-Vindo!";
            message.IsBodyHtml = true;
            message.Body = Email + " agora vaaaiiiii";

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("Turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(message);
        }

        //private string EmailBoasVindas(string nome)
        //{
        //    StreamReader leitor = new StreamReader(@"C:\Users\Claud\source\repos\Claudio-lunardi\CAR-LOCADORA\CarLocadora.EnviarEmail\TemplateEmail\TemplateEmail.cshtml", Encoding.UTF8);
        //    var conteudo = leitor.ReadToEnd();
        //    var TemplateEmail = conteudo.Replace("Nome¢", nome);

        //    //StringBuilder sb = new StringBuilder();
        //    //sb.Append($"<p>Parabéns <b>{nome},</b></p>");
        //    //sb.Append($"<p>Seja muito bem-vindo a <b>CAR-LOCADORA.</b></p>");
        //    //sb.Append($"<p>Estamos muito felizes de você fazer parte da <b>CAR-LOCADORA</b>.</p>");
        //    //sb.Append($"<br>");
        //    //sb.Append($"<p>Grande abraço</p>");

        //    return TemplateEmail;
        //}
        #endregion



    }
}