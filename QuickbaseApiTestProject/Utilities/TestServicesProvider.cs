using Microsoft.Extensions.DependencyInjection;

namespace QuickbaseApiTestProject.Utilities;

public class TestServicesProvider
{
    public static IServiceProvider ServiceProvider { get; private set; }
    
    public static void Initialize()
    {
        ServiceProvider = SetupDependencies.ConfigureServices();
    }
    
    public static T GetService<T>() => ServiceProvider.GetRequiredService<T>();
}