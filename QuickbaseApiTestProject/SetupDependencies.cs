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
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var services = new ServiceCollection();
        // 2. Bind the configuration to our settings model
        services.Configure<AppOptions>(config => configuration.Bind(config));

        // 3. Register our API config provider
        services.AddSingleton<IApiConfigProvider, ApiConfigProvider>();
        services.AddSingleton<IDataDriver, DataDriver>();
        
        // 4. Register HTTP client using the config provider
        RegisterQuickBaseApi(services, configuration);

        return services.BuildServiceProvider();
    }
    
    private static void RegisterQuickBaseApi(IServiceCollection services, IConfiguration configuration)
    {
        // Get API mode from configuration
        string apiMode = configuration["ApiMode"]!;
    
        // Register the appropriate implementation based on ApiMode
        if (string.Equals(apiMode, ApiMode.XML, StringComparison.OrdinalIgnoreCase))
        {
            // Register XML implementation with HttpClient
            services.AddHttpClient<IQuickBaseApi, XmlQuickBaseApi>(client =>
            {
                // Configure HttpClient for XML API
                var xmlApiUrl = configuration["XmlApiConfig:BaseApiUrl"];
                if (!string.IsNullOrEmpty(xmlApiUrl))
                {
                    client.BaseAddress = new Uri(xmlApiUrl);
                }
            
                // Add any XML-specific headers or configuration
            });
            // services.AddSingleton<IApiConfigProvider, ApiConfigProvider>();
        }
        else
        {
            // Register JSON implementation with HttpClient (default)
            services.AddHttpClient<IQuickBaseApi, JsonQuickBaseApi>(client =>
            {
                // Configure HttpClient for JSON API
                var jsonApiUrl = configuration["JsonApiConfig:BaseApiUrl"];
                if (!string.IsNullOrEmpty(jsonApiUrl))
                {
                    client.BaseAddress = new Uri(jsonApiUrl);
                }
            
                // Add JSON-specific headers
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            
                var realmHostname = configuration["JsonApiConfig:RealmHostname"];
                if (!string.IsNullOrEmpty(realmHostname))
                {
                    client.DefaultRequestHeaders.Add("QB-Realm-Hostname", realmHostname);
                }
            });
        }
    }

}