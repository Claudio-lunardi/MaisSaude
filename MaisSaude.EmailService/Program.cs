using MaisSaude.Common;
using MaisSaude.EmailService;
using MaisSaude.Infra.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
   .ConfigureServices((hostContext, services) =>
   {


       #region RabbitMQ
       services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
       services.AddSingleton<RabbitMQFactory>();
       #endregion



       services.AddHostedService<Worker>();
   })
    .Build();

await host.RunAsync();
