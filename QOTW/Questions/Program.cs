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
            var client = new MongoClient("mongodb://localhost:27017");
            return client;
        });
    })
    .Build();

host.Run();