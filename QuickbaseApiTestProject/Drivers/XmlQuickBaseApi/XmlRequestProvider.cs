using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using QuickbaseApiTestProject.DTOs.RequestDTOs;
using QuickbaseApiTestProject.TestUtilities;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;

public class XmlRequestProvider
{
    private readonly TestRunConfig testRunConfig;
    private readonly TestRunContext testRunContext;

    public XmlRequestProvider(IOptions<TestRunConfig> testSettings, TestRunContext testRunContext)
    {
        this.testRunConfig = testSettings.Value;
        this.testRunContext = testRunContext;
    }
    
    public AuthenticateRequestDto AuthenticateRequest(Action<AuthenticateRequestDto>? modifyRequest = null)
    {
        var request =  new AuthenticateRequestDto
        {
            Username = testRunConfig.Username,
            Password = testRunConfig.Password,
            Hours = 4,
            UserData = "requested from automated test"
        };
        
        modifyRequest?.Invoke(request);
        return request;
    }
    
    
    public AddRecordRequestDto AddRecordRequest(Action<AddRecordRequestDto>? modifyRequest = null, bool withNames = true)
    {
        var request =  new AddRecordRequestDto
        {
            UserData = CommonConstants.UserData,
            Ticket = testRunContext.Ticket,
            AppToken = testRunConfig.AppToken,
            Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>()
        };

        if (withNames)
        {
            request.AddNameField(XmlElementNames.Record.FirstName, "TestFirstName");
            request.AddNameField(XmlElementNames.Record.LastName, "TestLastName");
            request.AddNameField(XmlElementNames.Record.Age, "25");
            request.AddNameField(XmlElementNames.Record.DateOfBirth, "01-01-1980");
            request.AddNameField(XmlElementNames.Record.WebsiteUrl, "https://www.linkedin.com/feed/");
            request.AddNameField(XmlElementNames.Record.EmailAddress, DataGenerator.Email());
            request.AddNameField(XmlElementNames.Record.Mobile, "(25) 412-3123"); // to test with value "254123123"
        }
        else
        {
            request.AddFidField(XmlElementNames.Record.Id.FirstName, "TestFirstName");
            request.AddFidField(XmlElementNames.Record.Id.LastName, "TestLastName");
            request.AddFidField(XmlElementNames.Record.Id.Age, "25");
            request.AddFidField(XmlElementNames.Record.Id.DateOfBirth, "01-01-1980");
            request.AddFidField(XmlElementNames.Record.Id.WebsiteUrl, "https://www.linkedin.com/feed/");
            request.AddFidField(XmlElementNames.Record.Id.EmailAddress, DataGenerator.Email());
            request.AddFidField(XmlElementNames.Record.Id.Mobile, "(25) 412-3123"); // to test with value "254123123"
        }
        
        modifyRequest?.Invoke(request);
        return request;
    }
    
    public DoQueryRequestDto DoQueryRequest(Action<DoQueryRequestDto>? modifyRequest = null)
    {
        var request =  new DoQueryRequestDto
        {
            UserData = CommonConstants.UserData,
            Ticket = testRunContext.Ticket,
            AppToken = testRunConfig.AppToken,
            IncludeRecordId = 1
        };
        
        modifyRequest?.Invoke(request);
        return request;
    }
}