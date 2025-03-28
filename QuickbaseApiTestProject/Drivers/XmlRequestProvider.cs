using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.TestUtilities;

namespace QuickbaseApiTestProject.Drivers;

public class XmlRequestProvider
{
    private TestSettingsConfig testSettings;

    public XmlRequestProvider(IOptions<TestSettingsConfig> testSettings)
    {
        this.testSettings = testSettings.Value;
    }
    
    public AuthenticateRequestDto AuthenticateRequest(Action<AuthenticateRequestDto>? modifyRequest = null)
    {
        var request =  new AuthenticateRequestDto
        {
            Username = testSettings.Username,
            Password = testSettings.Password,
            Hours = 1,
            UserData = "requested from automated test"
        };
        
        modifyRequest?.Invoke(request);
        return request;
    }
}