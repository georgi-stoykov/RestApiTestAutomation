using System.Net;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.Drivers;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities.Constants;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
[TestFixture]
public class AddRecordSuccessTests
{
    private IQuickbaseApi quickbaseApi;
    // private readonly TestContext testContext;
    private XmlRequestProvider requestProvider;
    
    [SetUp]
    public void SetUp()
    {
        quickbaseApi = TestServicesProvider.GetService<IQuickbaseApi>();
        requestProvider = TestServicesProvider.GetService<XmlRequestProvider>();
    }

    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_OnlyMandatoryFieldIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, string>
        {
            ["firstname"] = "georgi",
            ["lastname"] = "Test6",
            ["age"] = "25",
        };
        var response = await quickbaseApi.AddRecordAsync("buzhrg7mn", request);

        AssertSuccessProperties(response);
        // Assert db records were increased by 1
        // Assert record properties non-mandatory are set to their default values
    }

    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_OnlyMandatoryFieldNames_Successfully()
    {
        true.Should().BeTrue();
        // non-mandatory are set to their default values
    }

    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_MandatoryAndOptionalFieldIDs_Successfully()
    {
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_MandatoryAndOptionalFieldNames_Successfully()
    {
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_MixtureOfOnlyMandatoryFieldNamesAndIDs_Successfully()
    {
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_WriteInBuiltInField_EnabledErrorIgnore_Successfully()
    {
        // Should record be added?
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_WriteInNonWritableField_CallIsIgnored()
    {
        // Unclear how what it means : "If you want to add data to these, you must write to those table fields; the vCard and iCalendar fields will be updated with that data."
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_InvalidDataForFieldType_MandatoryField_TolerantValidation_AddedSuccessfully()
    {
        //test with email
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_InvalidDataForFieldType_OptionalField_TolerantValidation_AddedSuccessfully()
    {
        //test with phone number, added with default field values
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_InvalidDataForFieldType_OptionalField_IntolerantValidation_AddedSuccessfully()
    {
        //test with phone number, added with default field values
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_WithDucplicateField_Unknown()
    {
        //test with phone number, added with default field values
        true.Should().BeTrue();
    }
    
    private static void AssertSuccessProperties(BaseResponse<AddRecordResponseDto> response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Action.Should().Be(ApiActionsEnum.API_AddRecord.ToString());
        response.Body.ErrorCode.Should().Be(0);
        response.Body.ErrorText.Should().Be("No error");
        response.Body.UserData.Should().Be("mydata");
        response.Body.RecordId.Should().BePositive();
        response.Body.UpdateId.Should().NotBe(null);
    }
}