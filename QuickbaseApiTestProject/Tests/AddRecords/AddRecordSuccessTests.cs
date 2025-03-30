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
        request.Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>();
        request.AddFieldAsId(XmlElementNames.Record.Id.FirstName, "TestFirstName");
        request.AddFieldAsId(XmlElementNames.Record.Id.LastName, "TestLastName");
        request.AddFieldAsId(XmlElementNames.Record.Id.Age, "25");
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithIdsAsync(response.Body.RecordId, request);
    }

    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_OnlyMandatoryFieldNames_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request);
    }

    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_MandatoryAndOptionalFieldIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest(withNames: false);
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithIdsAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_MandatoryAndOptionalFieldNames_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_MixtureOfOnlyMandatoryFieldNamesAndIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsId(XmlElementNames.Record.Id.Age, "25");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_WriteInBuiltInField_EnabledErrorIgnore_Successfully()
    {
        // Should record be added?
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_WriteInNonWritableField_CallIsIgnored()
    {
        // Unclear how what it means : "If you want to add data to these, you must write to those table fields; the vCard and iCalendar fields will be updated with that data."
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_InvalidDataForFieldType_MandatoryField_TolerantValidation_AddedSuccessfully()
    {
        //test with email
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_InvalidDataForFieldType_OptionalField_TolerantValidation_AddedSuccessfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsName(XmlElementNames.Record.EmailAddress, "invalidEmailFormat");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    // ToDo: Possible bug. The "Mobile" field is of type "Phone Number" but it is not required. According to the documentation the field should be ignored and record be created.
    public async Task AddRecord_InvalidDataForFieldType_OptionalField_StrictValidation_AddedSuccessfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsName(XmlElementNames.Record.Mobile, "invalidMobileNumberFormat");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request); // ToDo: check if assertion will catch this
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public async Task AddRecord_WithDuplicateField_Unknown()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new Dictionary<string, AddRecordRequestDto.FieldInfo>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsName(XmlElementNames.Record.Age, "30");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        var newRecord = await GetCreatedRecordAsync(x => x.RecordId == response.Body.RecordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.Age == 30);
    }
    
    private void AssertSuccessResponseProperties(BaseResponse<AddRecordResponseDto> response)
    {
        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.Body.Action == ApiAction.API_AddRecord.ToString());
        Assert.That(response.Body.ErrorCode == Constants.ErrorCode.NoError);
        Assert.That(response.Body.ErrorText == Constants.ErrorText.NoError);
        Assert.That(response.Body.UserData == Constants.UserData);
        Assert.That(response.Body.RecordId > 0);
        Assert.That(response.Body.UpdateId >= 0); // FIX ME
    }
    
    #region Assertions (ToDo: Find way to merge the 3 assertion methods into one elegant method. Ideally, we should use "ResponseValidator" class per request type that would handle everything)
    private async Task AssertRecordHasBeenAddedWithIdsAsync(int recordId, AddRecordRequestDto expectedRecord)
    {
        var newRecord = await GetCreatedRecordAsync(x => x.RecordId == recordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.FirstName == expectedRecord.Fields[XmlElementNames.Record.Id.FirstName].Value);
        Assert.That(newRecord!.LastName == expectedRecord.Fields[XmlElementNames.Record.Id.LastName].Value);
        Assert.That(newRecord!.Age == int.Parse(expectedRecord.Fields[XmlElementNames.Record.Id.Age].Value));
        
        // Assert.That(newRecord!.DateOfBirth == (requestDto.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.DateOfBirth)?.Value ?? Constants.DefaultTableFieldValue.DateOfBirth));
        Assert.That(newRecord!.WebsiteUrl == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.WebsiteUrl)?.Value ?? Constants.DefaultFieldValue.WebsiteUrl) );
        Assert.That(newRecord!.EmailAddress == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.EmailAddress)?.Value ?? Constants.DefaultFieldValue.Email));
        Assert.That(newRecord!.Mobile == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.Mobile)?.Value ?? Constants.DefaultFieldValue.Mobile));
    }
    
    private async Task AssertRecordHasBeenAddedWithNamesAsync(int recordId, AddRecordRequestDto expectedRecord)
    {
        var newRecord = await GetCreatedRecordAsync(x => x.RecordId == recordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.FirstName == expectedRecord.Fields[XmlElementNames.Record.FirstName].Value);
        Assert.That(newRecord!.LastName == expectedRecord.Fields[XmlElementNames.Record.LastName].Value);
        Assert.That(newRecord!.Age == int.Parse(expectedRecord.Fields[XmlElementNames.Record.Age].Value));
        
        // Assert.That(newRecord!.DateOfBirth == (requestDto.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.DateOfBirth)?.Value ?? Constants.DefaultTableFieldValue.DateOfBirth));
        Assert.That(newRecord!.WebsiteUrl == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.WebsiteUrl)?.Value ?? Constants.DefaultFieldValue.WebsiteUrl) );
        Assert.That(newRecord!.EmailAddress == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.EmailAddress)?.Value ?? Constants.DefaultFieldValue.Email));
        Assert.That(newRecord!.Mobile == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Mobile)?.Value ?? Constants.DefaultFieldValue.Mobile));
    }
    
    private async Task AssertRecordHasBeenAddedAsync(int recordId, AddRecordRequestDto expectedRecord)
    {
        var newRecord = await GetCreatedRecordAsync(x => x.RecordId == recordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);

        // MandatoryFields
        Assert.That(newRecord!.FirstName == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.FirstName)?.Value ?? expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.FirstName)?.Value ?? string.Empty));
        Assert.That(newRecord!.LastName == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.LastName)?.Value ?? expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.LastName)?.Value ?? string.Empty));
        string? requestedAge = expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Age)?.Value ?? expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.Age)?.Value;
        Assert.That(newRecord!.Age == (requestedAge != null ?  int.Parse(requestedAge) :  0));
        
        // Optional fields
        // Assert.That(newRecord!.DateOfBirth == (requestDto.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.DateOfBirth)?.Value ?? requestDto.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.DateOfBirth)?.Value ?? Constants.DefaultTableFieldValue.DateOfBirth));
        Assert.That(newRecord!.WebsiteUrl == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.WebsiteUrl)?.Value ?? expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.WebsiteUrl)?.Value ?? Constants.DefaultFieldValue.WebsiteUrl));
        Assert.That(newRecord!.EmailAddress == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.EmailAddress)?.Value ?? expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.EmailAddress)?.Value ?? Constants.DefaultFieldValue.Email));
        Assert.That(newRecord!.Mobile == (expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Mobile)?.Value ?? expectedRecord.Fields.GetValueOrDefault<string, AddRecordRequestDto.FieldInfo>(XmlElementNames.Record.Id.Mobile)?.Value ?? Constants.DefaultFieldValue.Mobile));
    }
    #endregion
}