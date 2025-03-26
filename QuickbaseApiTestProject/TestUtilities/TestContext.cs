namespace QuickbaseApiTestProject.TestUtilities;

public record TestContext
{
    public required object RequestData { get; set; }
    public required object ResponseData { get; set; }
}

public record TestRunContext
{
    public required string AuthenticationToken { get; set; }
    public required string ApiMode { get; set; }
}
