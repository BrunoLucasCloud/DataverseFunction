using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataverseFunction;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddSingleton<AccountService>();
    })
    .Build();

host.Run();
