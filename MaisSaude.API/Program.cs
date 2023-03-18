//using MaisSaude.API.Extencoes;
using AspNetCoreRateLimit;
using MaisSaude.API.Extencoes;
using MaisSaude.Business.AgendamentoBuziness;
using MaisSaude.Business.ClinicaBuziness;
using MaisSaude.Business.DependenteBuziness;
using MaisSaude.Business.Login_home;
using MaisSaude.Business.MedicoBuziness;
using MaisSaude.Business.Rabbit;
using MaisSaude.Business.TitularBuziness;
using MaisSaude.Common;
using MaisSaude.Infra.Dapper;
using MaisSaude.Infra.RabbitMQ;
using MaisSaude.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarSwagger();
builder.Services.ConfigurarJwt();

builder.Services.ConfigureRateLimitingOptions();



builder.Services.AddCors();
#region RabbitMQ
builder.Services.Configure<DadosBaseRabbitMQ>(builder.Configuration.GetSection("DadosBaseRabbitMQ"));
builder.Services.AddScoped<IMensageria, Mensageria>();
builder.Services.AddSingleton<RabbitMQFactory>();
#endregion

builder.Services.AddSingleton<ConnectionDapper>();
builder.Services.AddScoped<ITitularBuziness, TitularBuziness>();
builder.Services.AddScoped<IDependenteBuziness, DependenteBuziness>();
builder.Services.AddScoped<IClinicaBuziness, ClinicaBuziness>();
builder.Services.AddScoped<ILogin, Login>();
builder.Services.AddScoped<IAgendamentoBuziness, AgendamentoBuziness>();
builder.Services.AddScoped<IMedicoBuziness, MedicoBuziness>();

builder.Services.AddControllers();

builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var url = "https://localhost:7247";
app.UseCors(b => b.WithOrigins(url));

app.UseAuthentication();

app.UseAuthorization();
app.UseIpRateLimiting();

app.MapControllers();
app.Run();