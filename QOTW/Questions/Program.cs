using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddSingleton(s =>
        {
            // TODO: Replace with env connectionstring
            var client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString"));
            return client;
        });
    })
    .Build();

host.Run();