using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.TestUtilities.Constants;

namespace QuickbaseApiTestProject.TestUtilities;

public record ApiConfigProvider(IOptions<AppOptions> Options) : IApiConfigProvider
{
    private readonly AppOptions _settings = Options.Value;

    public string BaseApiUrl => _settings.ApiMode == ApiMode.XML
        ? _settings.XmlApiConfig.BaseApiUrl 
        : _settings.JsonApiConfig.BaseApiUrl;

    public EndpointConfig Endpoints => _settings.ApiMode == ApiMode.XML 
        ? _settings.XmlApiConfig.Endpoints 
        : _settings.JsonApiConfig.Endpoints;
}