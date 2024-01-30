using MassTransit;
using TechChallenge2.Consumer;
using TechChallenge2.Consumer.Eventos;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var fila = configuration.GetSection("AzureServiceBus")["NomeFila"] ?? string.Empty;
        var conexao = configuration.GetSection("AzureServiceBus")["Conexao"] ?? string.Empty;

        services.AddHostedService<Worker>();

        services.AddMassTransit(x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(conexao);
                cfg.ReceiveEndpoint(fila, e =>
                {
                    e.Consumer<EnvioEmailConfirmacao>();
                });
            });
        });
    })
    .Build();

host.Run();
