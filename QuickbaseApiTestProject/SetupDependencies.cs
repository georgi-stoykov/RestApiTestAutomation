using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickbaseApiTestProject.Drivers;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.TestUtilities;
using QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject;

public class SetupDependencies
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        // 1. Build the Configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
        
        // 2. Bind the configuration settings
        services.AddOptions<TestRunConfig>()
            .Bind(configuration.GetSection(nameof(TestRunConfig)));

        services.AddOptions<ApiSettingsConfig>(nameof(ApiSettingsConfig.XmlApiConfig))
            .Bind(configuration.GetSection(nameof(ApiSettingsConfig.XmlApiConfig)));

        // 3. Register our API config provider  
        services.AddSingleton<XmlRequestProvider>();
        services.AddSingleton<TestRunContext>();
        
        // 4. Register HTTP client using the config provider`
        services.AddHttpClient<IQuickbaseApi, XmlQuickbaseApi>();

        return services.BuildServiceProvider();
    }
}