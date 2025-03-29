using Microsoft.Extensions.DependencyInjection;
using QuickbaseApiTestProject.Drivers;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject;

[SetUpFixture]
public class Hooks : IDisposable
{
    private readonly IServiceProvider serviceProvider;
    private XmlRequestProvider requestProvider;
    private IQuickbaseApi quickbaseApi;
    private bool disposed = false;

    public Hooks()
    {
        // Resolve the dependencies
        // serviceProvider = SetupDependencies.ConfigureServices();
        // requestProvider = serviceProvider.GetRequiredService<XmlRequestProvider>();
        // quickbaseApi = serviceProvider.GetRequiredService<IQuickbaseApi>();
    }

    [OneTimeSetUp]
    public async Task Setup()
    {
        TestServicesProvider.Initialize();
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