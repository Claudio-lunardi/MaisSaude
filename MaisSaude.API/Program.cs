//using MaisSaude.API.Extencoes;
using AspNetCoreRateLimit;
using MaisSaude.API.Extencoes;
using MaisSaude.Business.ClinicaBuziness;
using MaisSaude.Business.DependenteBuziness;
using MaisSaude.Business.Login_home;
using MaisSaude.Business.TitularBuziness;
using MaisSaude.Infra.Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarSwagger();
builder.Services.ConfigurarJwt();
builder.Services.ConfigureRateLimitingOptions();

builder.Services.AddSingleton<ConnectionDapper>();
builder.Services.AddScoped<ITitularBuziness, TitularBuziness>();
builder.Services.AddScoped<IDependenteBuziness, DependenteBuziness>();
builder.Services.AddScoped<IClinicaBuziness, ClinicaBuziness>();
builder.Services.AddScoped<ILogin, Login>();

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