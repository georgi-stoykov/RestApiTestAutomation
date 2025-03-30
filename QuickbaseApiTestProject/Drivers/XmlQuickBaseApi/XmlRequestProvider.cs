using Microsoft.Extensions.Options;
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
    
    
    public AddRecordRequestDto AddRecordRequest(Action<AddRecordRequestDto>? modifyRequest = null, bool useNameFields = true, bool includeOptionalFields = false)
    {
        // Extract repeating string values into private variables
        const string TestFirstNameValue = "TestFirstName";
        const string TestLastNameValue = "TestLastName";
        const string DefaultAgeValue = "25";
        const string DefaultDateOfBirthValue = "01-01-1980";
        const string DefaultWebsiteValue = "https://www.linkedin.com/feed/";
        const string DefaultMobileValue = "(25) 412-3123"; // to test with value "254123123"
        string workEmail = DataGenerator.NewEmail();
        string personalEmail = DataGenerator.NewEmail();
        
        var request =  new AddRecordRequestDto
        {
            UserData = Constants.UserData,
            Ticket = testRunContext.Ticket,
            AppToken = testRunConfig.AppToken,
            Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>(),
            IgnoreError = null
        };

        if (useNameFields)
        {
            request.AddFieldAsName(XmlElementNames.Record.FirstName, TestFirstNameValue);
            request.AddFieldAsName(XmlElementNames.Record.LastName, TestLastNameValue);
            request.AddFieldAsName(XmlElementNames.Record.Age, DefaultAgeValue);
            request.AddFieldAsName(XmlElementNames.Record.WorkEmail, workEmail);

            if (includeOptionalFields)
            {
                request.AddFieldAsName(XmlElementNames.Record.DateOfBirth, DefaultDateOfBirthValue);
                request.AddFieldAsName(XmlElementNames.Record.WebsiteUrl, DefaultWebsiteValue);
                request.AddFieldAsName(XmlElementNames.Record.PersonalEmail, personalEmail);
                request.AddFieldAsName(XmlElementNames.Record.Mobile, DefaultMobileValue);
            }
        }
        else
        {
            request.AddFieldAsId(XmlElementNames.Record.Id.FirstName, TestFirstNameValue);
            request.AddFieldAsId(XmlElementNames.Record.Id.LastName, TestLastNameValue);
            request.AddFieldAsId(XmlElementNames.Record.Id.Age, DefaultAgeValue);
            request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, workEmail);
            
            if (includeOptionalFields)
            {
                request.AddFieldAsId(XmlElementNames.Record.Id.DateOfBirth, DefaultDateOfBirthValue);
                request.AddFieldAsId(XmlElementNames.Record.Id.WebsiteUrl, DefaultWebsiteValue);
                request.AddFieldAsId(XmlElementNames.Record.Id.EmailAddress, personalEmail);
                request.AddFieldAsId(XmlElementNames.Record.Id.Mobile, DefaultMobileValue);
            }
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