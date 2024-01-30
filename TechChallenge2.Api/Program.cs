using MassTransit;
using TechChallenge.Api.IoC;
using TechChallenge2.Api.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.RegisterServices(builder.Configuration);

var configuration = builder.Configuration;
var conexao = configuration.GetSection("AzureServiceBus")["Conexao"] ?? string.Empty;

builder.Services.AddMassTransit((x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(conexao);
    });
}));

var app = builder.Build();


//Acionando o método de realizar as migrattions 
DataBaseManagementService.MigrationInitialisation(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
