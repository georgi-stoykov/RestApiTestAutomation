namespace QuickbaseApiTestProject.TestUtilities;

public interface IApiConfigProvider
{
    string BaseApiUrl { get; }
    EndpointConfig Endpoints { get; }
}
