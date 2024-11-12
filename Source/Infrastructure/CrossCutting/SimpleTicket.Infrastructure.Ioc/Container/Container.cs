using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace SimpleTicket.Infrastructure.Ioc.Container;

public class Container
{
    private readonly IServiceCollection _services;
    public readonly IConfiguration Configuration;
    private readonly string _environmentName;
    private ServiceProvider? _serviceProvider;

    public Container()
    {
        _environmentName = GetEnvironmentName();
        _services = new ServiceCollection();
        Configuration = BuildConfiguration();
        
        _services.AddSingleton(Configuration);
        _serviceProvider = null;
    }
    
    public string EnvironmentName => _environmentName;
    
    public ServiceProvider ServiceProvider => 
        _serviceProvider ?? throw new Exception("Container must be build");

    public void Build(Action<IServiceCollection, IConfiguration> options)
    {
        if(_serviceProvider != null)
            throw new InvalidOperationException("Container is already built.");
        
        options(_services, Configuration);
        _serviceProvider = _services.BuildServiceProvider();
    }
    
    private IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{_environmentName}.json", optional: true, reloadOnChange: true)
            .Build();
    }
    
    private string GetEnvironmentName()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "dev";
    }
}