using System.Net;
using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;
using QuickbaseApiTestProject.DTOs.RequestDTOs;
using QuickbaseApiTestProject.TestUtilities;
using QuickbaseApiTestProject.Utilities.ConfigDTOs;

namespace QuickbaseApiTestProject.Utilities;

public class TestRunContext
{
    private readonly IQuickbaseApi quickbaseApi;
    private readonly TestRunConfig testRunConfig;
    public string Ticket { get; private set; }
    
    public TestRunContext()
    {
        quickbaseApi = TestServicesProvider.GetService<IQuickbaseApi>();
        testRunConfig = TestServicesProvider.GetService<IOptions<TestRunConfig>>().Value;
        Ticket = AuthenticateForTestRunAsync();
    }
    
    private string AuthenticateForTestRunAsync()
    {
        var authRequest = new AuthenticateRequestDto
        {
            Username = testRunConfig.Username,
            Password = testRunConfig.Password,
            Hours = 4,
            UserData = "requested from automated test"
        };
        var response = quickbaseApi.AuthenticateAsync(authRequest).GetAwaiter().GetResult();
        Assert.That(response.StatusCode == HttpStatusCode.OK, string.Format(Constants.Constants.AssertionMessage.RequestFailed, response.Body.ErrorText));
        return response.Body.Ticket;
    }
}
