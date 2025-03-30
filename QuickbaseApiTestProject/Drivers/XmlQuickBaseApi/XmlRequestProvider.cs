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
            UserData = Constants.UserData,
            Ticket = testRunContext.Ticket,
            AppToken = testRunConfig.AppToken,
            Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>()
        };

        if (withNames)
        {
            request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
            request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
            request.AddFieldAsName(XmlElementNames.Record.Age, "25");
            request.AddFieldAsName(XmlElementNames.Record.DateOfBirth, "01-01-1980");
            request.AddFieldAsName(XmlElementNames.Record.WebsiteUrl, "https://www.linkedin.com/feed/");
            request.AddFieldAsName(XmlElementNames.Record.EmailAddress, DataGenerator.Email());
            request.AddFieldAsName(XmlElementNames.Record.Mobile, "(25) 412-3123"); // to test with value "254123123"
        }
        else
        {
            request.AddFieldAsId(XmlElementNames.Record.Id.FirstName, "TestFirstName");
            request.AddFieldAsId(XmlElementNames.Record.Id.LastName, "TestLastName");
            request.AddFieldAsId(XmlElementNames.Record.Id.Age, "25");
            request.AddFieldAsId(XmlElementNames.Record.Id.DateOfBirth, "01-01-1980");
            request.AddFieldAsId(XmlElementNames.Record.Id.WebsiteUrl, "https://www.linkedin.com/feed/");
            request.AddFieldAsId(XmlElementNames.Record.Id.EmailAddress, DataGenerator.Email());
            request.AddFieldAsId(XmlElementNames.Record.Id.Mobile, "(25) 412-3123"); // to test with value "254123123"
        }
        
        modifyRequest?.Invoke(request);
        return request;
    }
    
    public DoQueryRequestDto DoQueryRequest(Action<DoQueryRequestDto>? modifyRequest = null)
    {
        var request =  new DoQueryRequestDto
        {
            UserData = Constants.UserData,
            Ticket = testRunContext.Ticket,
            AppToken = testRunConfig.AppToken,
            IncludeRecordId = 1
        };
        
        modifyRequest?.Invoke(request);
        return request;
    }
}