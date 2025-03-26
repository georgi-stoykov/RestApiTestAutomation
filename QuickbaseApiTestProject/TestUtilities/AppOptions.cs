namespace QuickbaseApiTestProject.TestUtilities;

public record AppOptions
{
    public readonly string ApiMode;
    public readonly XmlApiConfig XmlApiConfig;
    public readonly JsonApiConfig JsonApiConfig;
}

public record XmlApiConfig
{
    public readonly string BaseApiUrl;
    public readonly EndpointConfig Endpoints;
}

public record JsonApiConfig
{
    public readonly string BaseApiUrl;
    public readonly EndpointConfig Endpoints;
}

public record EndpointConfig
{
    public readonly string Authenticate;
    public readonly string AddRecord;
    public readonly string DeleteRecord;
    public readonly string PurgeRecords;
    public readonly string EditRecord;
}
