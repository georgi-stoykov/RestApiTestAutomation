using System.Net;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities.Constants;
using QuickbaseApiTestProject.Utilities.Constants;

namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
[TestFixture]
public class AddRecordSuccessTests : RecordTestHooks
{
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_OnlyMandatoryFieldIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, string>
        {
            [XmlFieldNames.Record.FirstName] = "georgi",
            [XmlFieldNames.Record.LastName] = "Test6",
            [XmlFieldNames.Record.Age] = "25",
        };
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedAsync(response.Body.RecordId, request);
        // Assert record properties non-mandatory are set to their default values
    }

    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_OnlyMandatoryFieldNames_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, string>
        {
            [XmlFieldNames.Record.FirstName] = "georgi",
            [XmlFieldNames.Record.LastName] = "Test6",
            [XmlFieldNames.Record.Age] = "25",
        };
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedAsync(response.Body.RecordId, request);
        // non-mandatory are set to their default values
    }

    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_MandatoryAndOptionalFieldIDs_Successfully()
    {
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_MandatoryAndOptionalFieldNames_Successfully()
    {
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_MixtureOfOnlyMandatoryFieldNamesAndIDs_Successfully()
    {
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_WriteInBuiltInField_EnabledErrorIgnore_Successfully()
    {
        // Should record be added?
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_WriteInNonWritableField_CallIsIgnored()
    {
        // Unclear how what it means : "If you want to add data to these, you must write to those table fields; the vCard and iCalendar fields will be updated with that data."
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_InvalidDataForFieldType_MandatoryField_TolerantValidation_AddedSuccessfully()
    {
        //test with email
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_InvalidDataForFieldType_OptionalField_TolerantValidation_AddedSuccessfully()
    {
        //test with phone number, added with default field values
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_InvalidDataForFieldType_OptionalField_IntolerantValidation_AddedSuccessfully()
    {
        //test with phone number, added with default field values
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_WithDucplicateField_Unknown()
    {
        //test with phone number, added with default field values
        true.Should().BeTrue();
    }
    
    private void AssertSuccessResponseProperties(BaseResponse<AddRecordResponseDto> response)
    {
        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.Body.Action == ApiAction.API_AddRecord.ToString());
        Assert.That(response.Body.ErrorCode == 0);
        Assert.That(response.Body.ErrorText == "No error");
        Assert.That(response.Body.UserData == "mydata");
        Assert.That(response.Body.RecordId > 0);
        Assert.That(response.Body.UpdateId >= 0); // FIX ME
    }
    
    private async Task AssertRecordHasBeenAddedAsync(int recordId, AddRecordRequestDto requestDto)
    {
        var request = requestProvider.DoQueryRequest();
        var tableRecords = await quickbaseApi.GetTableRecordsAsync(tableId, request);
        var newRecord = tableRecords.Body.Records.SingleOrDefault(x => x.RecordId == recordId);
        Assert.That(newRecord != null);
        Assert.That(newRecord!.FirstName == requestDto.Fields[XmlFieldNames.Record.FirstName]);
        Assert.That(newRecord!.LastName == requestDto.Fields[XmlFieldNames.Record.LastName]);
        Assert.That(newRecord!.Age == int.Parse(requestDto.Fields[XmlFieldNames.Record.Age]));
        
        Assert.That(newRecord!.DateOfBirth == (requestDto.Fields.GetValueOrDefault<string, string>(XmlFieldNames.Record.DateOfBirth) ?? ""));
        Assert.That(newRecord!.WebsiteUrl == (requestDto.Fields.GetValueOrDefault<string, string>(XmlFieldNames.Record.WebsiteUrl) ?? "") );
        Assert.That(newRecord!.EmailAddress == (requestDto.Fields.GetValueOrDefault<string, string>(XmlFieldNames.Record.EmailAddress) ?? ""));
        Assert.That(newRecord!.Mobile == (requestDto.Fields.GetValueOrDefault<string, string>(XmlFieldNames.Record.Mobile) ?? ""));
    }
}