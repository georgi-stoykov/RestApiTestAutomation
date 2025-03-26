using Microsoft.Extensions.DependencyInjection;
using QuickbaseApiTestProject.Drivers.Interfaces;

namespace QuickbaseApiTestProject;

[SetUpFixture]
public class Hooks : IDisposable
{
    private readonly IServiceProvider serviceProvider;
    private IDataDriver dataDriver;
    private IQuickBaseApi quickBaseApi;
    private bool disposed = false;

    public Hooks()
    {
        // Resolve the dependencies
        serviceProvider = SetupDependencies.ConfigureServices();
        dataDriver = serviceProvider.GetRequiredService<IDataDriver>();
        quickBaseApi = serviceProvider.GetRequiredService<IQuickBaseApi>();
    }

    [OneTimeSetUp]
    public async Task Setup()
    {
        var request = dataDriver.AuthenticateRequest();
        var response = await quickBaseApi.AuthenticateAsync(string.Empty);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose of ServiceProvider if it implements IDisposable
                if (serviceProvider is IDisposable disposableProvider)
                {
                    disposableProvider.Dispose();
                }
            }

            disposed = true;
        }
    }


}