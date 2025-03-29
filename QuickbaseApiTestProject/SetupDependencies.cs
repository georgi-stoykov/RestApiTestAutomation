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
        services.AddOptions<TestSettingsConfig>()
            .Bind(configuration.GetSection(nameof(TestSettingsConfig)));

        services.AddOptions<ApiSettingsConfig>(nameof(ApiSettingsConfig.XmlApiConfig))
            .Bind(configuration.GetSection(nameof(ApiSettingsConfig.XmlApiConfig)));

        services.AddOptions<ApiSettingsConfig>(nameof(ApiSettingsConfig.JsonApiConfig))
            .Bind(configuration.GetSection(nameof(ApiSettingsConfig.JsonApiConfig)));

        // 3. Register our API config provider  
        services.AddSingleton<XmlRequestProvider>();
        
        // 4. Register HTTP client using the config provider`
        RegisterQuickBaseApi(services, configuration);

        return services.BuildServiceProvider();
    }
    
    private static void RegisterQuickBaseApi(IServiceCollection services, IConfiguration configuration)
    {
        // Get API mode from configuration
        var apiMode =
            configuration.GetSection(nameof(TestSettingsConfig))[nameof(TestSettingsConfig.ApiMode)];
    
        // Register the appropriate implementation based on ApiMode
        if (string.Equals(apiMode, ApiMode.XML, StringComparison.OrdinalIgnoreCase))
        {
            services.AddHttpClient<IQuickbaseApi, XmlQuickbaseApi>();
        }
        else
        {
            // services.AddHttpClient<IQuickbaseApi, JsonQuickbaseApi>();
        }
    }
}