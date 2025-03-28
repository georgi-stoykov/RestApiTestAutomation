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
    
    public AddRecordRequestDto AddRecordRequest(Action<AddRecordRequestDto>? modifyRequest = null)
    {
        var request =  new AddRecordRequestDto
        {
            UserData = "mydata",
            Ticket = testSettings.Ticket,
            AppToken = testSettings.AppToken,
            Fields = new Dictionary<string, string>
            {
                ["firstname"] = "georgi",
                ["lastname"] = "Test6",
                ["age"] = "25",
                ["date_of_birth"] = "06-03-1980",
                ["website_url"] = "https://www.linkedin.com/feed/",
                ["email_address"] = "georgi4@georgistoykov.com",
                ["mobile"] = "123123123"
            }
        };
        
        modifyRequest?.Invoke(request);
        return request;
    }
}