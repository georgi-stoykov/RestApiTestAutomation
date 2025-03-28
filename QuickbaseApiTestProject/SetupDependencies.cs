using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using QuickbaseApiTestProject.Drivers;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.TestUtilities;
using Microsoft.Extensions.Http;
using QuickbaseApiTestProject.TestUtilities.Constants;

namespace QuickbaseApiTestProject;

public class SetupDependencies
{
    public static ServiceProvider ConfigureServices()
    {
        // 1. Build the Configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
        
        var services = new ServiceCollection();
        
        // 2. Bind the configuration settings
        services.AddOptions<TestSettingsConfig>()
            .Bind(configuration.GetSection(nameof(TestSettingsConfig.ApiMode)))
            .Validate(x => x.ApiMode is ApiMode.XML or ApiMode.JSON);

        services.AddOptions<ApiSettingsConfig>(nameof(ApiSettingsConfig.XmlApiConfig))
            .Bind(configuration.GetSection(nameof(ApiSettingsConfig.XmlApiConfig)));

        services.AddOptions<ApiSettingsConfig>(nameof(ApiSettingsConfig.JsonApiConfig))
            .Bind(configuration.GetSection(nameof(ApiSettingsConfig.JsonApiConfig)));

#if DEBUG
        var viewSettings = configuration.GetDebugView();
#endif

        // 3. Register our API config provider
        services.AddSingleton<IDataDriver, DataDriver>();
        
        // 4. Register HTTP client using the config provider`
        RegisterQuickBaseApi(services, configuration);

        return services.BuildServiceProvider();
    }
    
    private static void RegisterQuickBaseApi(IServiceCollection services, IConfiguration configuration)
    {
        // Get API mode from configuration
        string? apiMode = configuration.GetValue<string>(nameof(TestSettingsConfig.ApiMode));
    
        // Register the appropriate implementation based on ApiMode
        if (string.Equals(apiMode, ApiMode.XML, StringComparison.OrdinalIgnoreCase))
        {
            services.AddHttpClient<IQuickBaseApi, XmlQuickBaseApi>();
        }
        else
        {
            services.AddHttpClient<IQuickBaseApi, JsonQuickBaseApi>();
        }
        
        // // Register the appropriate implementation based on ApiMode
        // if (string.Equals(apiMode, ApiMode.XML, StringComparison.OrdinalIgnoreCase))
        // {
        //     // Register XML implementation with HttpClient
        //     services.AddHttpClient<IQuickBaseApi, XmlQuickBaseApi>(client =>
        //     {
        //         // Configure HttpClient for XML API
        //         var xmlApiUrl = configuration["XmlApiConfig:BaseApiUrl"];
        //         if (!string.IsNullOrEmpty(xmlApiUrl))
        //         {
        //             client.BaseAddress = new Uri(xmlApiUrl);
        //         }
        //     
        //         // Add any XML-specific headers or configuration
        //     });
        // }
        // else
        // {
        //     // Register JSON implementation with HttpClient (default)
        //     services.AddHttpClient<IQuickBaseApi, JsonQuickBaseApi>(client =>
        //     {
        //         // Configure HttpClient for JSON API
        //         var jsonApiUrl = configuration["JsonApiConfig:BaseApiUrl"];
        //         if (!string.IsNullOrEmpty(jsonApiUrl))
        //         {
        //             client.BaseAddress = new Uri(jsonApiUrl);
        //         }
        //     
        //         // Add JSON-specific headers
        //         client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        //     
        //         var realmHostname = configuration["JsonApiConfig:RealmHostname"];
        //         if (!string.IsNullOrEmpty(realmHostname))
        //         {
        //             client.DefaultRequestHeaders.Add("QB-Realm-Hostname", realmHostname);
        //         }
        //     });
        // }
    }
}