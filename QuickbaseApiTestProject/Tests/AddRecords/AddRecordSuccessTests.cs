﻿using System.Net;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities.Constants;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject.Tests.AddRecords;

public class AddRecordSuccessTests : RecordTests
{
    [Test(Description = "Record can be successfully added using only mandatory fields passed as ids")]
    public async Task AddRecord_OnlyMandatoryFieldIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest(useNameFields: false, includeOptionalFields: false);
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithIdsAsync(response.Body.RecordId, request);
    }

    [Test(Description = "Record can be successfully added using only mandatory fields passed as names")]
    public async Task AddRecord_OnlyMandatoryFieldNames_Successfully()
    {
        var request = requestProvider.AddRecordRequest(useNameFields: true, includeOptionalFields: false);
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request);
    }

    [Test(Description = "Record can be successfully added using all mandatory and optional fields passed as ids")]
    public async Task AddRecord_MandatoryAndOptionalFieldIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest(useNameFields: false, includeOptionalFields: true);
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithIdsAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "Record can be successfully added using all mandatory and optional fields passed as names")]
    public async Task AddRecord_MandatoryAndOptionalFieldNames_Successfully()
    {
        var request = requestProvider.AddRecordRequest(useNameFields: true, includeOptionalFields: true);
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "Record can be successfully added using mixture of field names and ids")]
    public async Task AddRecord_MixtureOfOnlyMandatoryFieldNamesAndIDs_Successfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsId(XmlElementNames.Record.Id.Age, "25");
        request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, DataGenerator.NewEmail());

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "When ignoring of errors is enabled, record can be successfully created even when attempting to pass value to built-in fields")]
    public async Task AddRecord_WriteInBuiltInField_EnabledErrorIgnore_Successfully()
    {
        var request = requestProvider.AddRecordRequest();

        var valueForBuildInField = 99999999;
        request.AddFieldAsId(XmlElementNames.Record.Id.BuildIn_RecordID, valueForBuildInField.ToString());
        request.IgnoreError = 1;      
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedAsync(response.Body.RecordId, request);
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == valueForBuildInField);
        Assert.That(newRecord == null, Constants.AssertionMessage.UnexpectedRecord);
    }
    
    [Test(Description = "When ignoring of errors is enabled, record can be successfully created even when attempting to pass value to non-writable fields")]
    public async Task AddRecord_WriteInNonWritableField_CallIsIgnored()
    {
        var request = requestProvider.AddRecordRequest();

        var valueForNonWritableField = DataGenerator.AlphaNumberString();
        request.AddFieldAsId(XmlElementNames.Record.Id.NonWritable_vCardField, valueForNonWritableField);
        request.IgnoreError = 1;      
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedAsync(response.Body.RecordId, request);
    }
    
    [Test(Description = "Record is successfully created even when invalid data is passed to mandatory field with tolerant data type validation")]
    public async Task AddRecord_InvalidDataForFieldType_MandatoryField_TolerantValidation_AddedSuccessfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");

        var invalidMandatoryFieldValue = DataGenerator.AlphaNumberString();
        request.AddFieldAsName(XmlElementNames.Record.WorkEmail, invalidMandatoryFieldValue);
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertSuccessResponseProperties(response);
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == response.Body.RecordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.WorkEmail == invalidMandatoryFieldValue);
    }
    
    [Test(Description = "Record is successfully created even when invalid data is passed to optional field with tolerant data type validation")]
    public async Task AddRecord_InvalidDataForFieldType_OptionalField_TolerantValidation_AddedSuccessfully()
    {
        var request = requestProvider.AddRecordRequest();
        string invalidOptionalFieldValue = DataGenerator.AlphaNumberString();
        request.AddFieldAsName(XmlElementNames.Record.PersonalEmail, invalidOptionalFieldValue);

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertSuccessResponseProperties(response);
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == response.Body.RecordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.PersonalEmail == invalidOptionalFieldValue);
    }
    
    [Test(Description = "Record is successfully created even when invalid data is passed to optional field with strict data type validation")]
    // ToDo: Ask if possible bug. The "Mobile" field is of type "Phone Number" but it is not required. According to the documentation the field should be ignored and record be created.
    public async Task AddRecord_InvalidDataForFieldType_OptionalField_StrictValidation_AddedSuccessfully()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, DataGenerator.NewEmail());
        request.AddFieldAsName(XmlElementNames.Record.Mobile, "invalidMobileNumberFormat");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        await AssertRecordHasBeenAddedWithNamesAsync(response.Body.RecordId, request); // ToDo: check if assertion will catch this
    }
    
    [Test(Description = "When passing the same field name twice on data creation, the record is created with the last field value")]
    public async Task AddRecord_WithDuplicateField_Success()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, DataGenerator.NewEmail());
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsName(XmlElementNames.Record.Age, "30");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertSuccessResponseProperties(response);
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == response.Body.RecordId);
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
        Assert.That(response.Body.UpdateId > 0);
    }
    
    // ToDo: Find way to merge the 3 assertion methods into one elegant method. Ideally, we should use "ResponseValidator" class per request type that would handle everything)
    private async Task AssertRecordHasBeenAddedWithIdsAsync(int recordId, AddRecordRequestDto expectedRecord)
    {
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == recordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.FirstName == expectedRecord.GetField(XmlElementNames.Record.Id.FirstName).Value);
        Assert.That(newRecord!.LastName == expectedRecord.GetField(XmlElementNames.Record.Id.LastName).Value);
        Assert.That(newRecord!.Age == int.Parse(expectedRecord.GetField(XmlElementNames.Record.Id.Age).Value));
        Assert.That(newRecord!.WorkEmail == expectedRecord.GetField(XmlElementNames.Record.Id.WorkEmail).Value);
        
        // Assert.That(newRecord!.DateOfBirth == (requestDto.GetFieldOrDefault(XmlElementNames.Record.Id.DateOfBirth)?.Value ?? Constants.DefaultTableFieldValue.DateOfBirth));
        Assert.That(newRecord!.WebsiteUrl == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.WebsiteUrl)?.Value ?? Constants.DefaultFieldValue.WebsiteUrl) );
        Assert.That(newRecord!.PersonalEmail == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.EmailAddress)?.Value ?? Constants.DefaultFieldValue.PersonalEmail));
        Assert.That(newRecord!.Mobile == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.Mobile)?.Value ?? Constants.DefaultFieldValue.Mobile));
        Assert.That(newRecord.VCardField == null);
    }
    
    private async Task AssertRecordHasBeenAddedWithNamesAsync(int recordId, AddRecordRequestDto expectedRecord)
    {
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == recordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
        Assert.That(newRecord!.FirstName == expectedRecord.GetField(XmlElementNames.Record.FirstName).Value);
        Assert.That(newRecord!.LastName == expectedRecord.GetField(XmlElementNames.Record.LastName).Value);
        Assert.That(newRecord!.Age == int.Parse(expectedRecord.GetField(XmlElementNames.Record.Age).Value));
        Assert.That(newRecord!.WorkEmail == expectedRecord.GetField(XmlElementNames.Record.WorkEmail).Value);
        
        // Didn't have time to work on parsing the Date from milliseconds to ordinary date. I saw I can take the ordinary value (dd-mm-yyyy) from request "API_GetRecordInfo" but didn't have time for that also 
        // Assert.That(newRecord!.DateOfBirth == (requestDto.GetFieldOrDefault(XmlElementNames.Record.DateOfBirth)?.Value ?? Constants.DefaultTableFieldValue.DateOfBirth)); 
        Assert.That(newRecord!.WebsiteUrl == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.WebsiteUrl)?.Value ?? Constants.DefaultFieldValue.WebsiteUrl) );
        Assert.That(newRecord!.PersonalEmail == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.PersonalEmail)?.Value ?? Constants.DefaultFieldValue.PersonalEmail));
        Assert.That(newRecord!.Mobile == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Mobile)?.Value ?? Constants.DefaultFieldValue.Mobile));
        Assert.That(newRecord.VCardField == null);
    }
    
    private async Task AssertRecordHasBeenAddedAsync(int recordId, AddRecordRequestDto expectedRecord)
    {
        var newRecord = await GetTableRecordsAsync(x => x.RecordId == recordId);
        Assert.That(newRecord != null, Constants.AssertionMessage.MissingExpectedRecord);

        // MandatoryFields
        Assert.That(newRecord!.FirstName == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.FirstName)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.FirstName)?.Value ?? string.Empty));
        Assert.That(newRecord!.LastName == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.LastName)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.LastName)?.Value ?? string.Empty));
        string? requestedAge = expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Age)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.Age)?.Value;
        Assert.That(newRecord!.Age == (requestedAge != null ?  int.Parse(requestedAge) :  0));
        Assert.That(newRecord!.WorkEmail == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.WorkEmail)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.WorkEmail)?.Value ?? string.Empty));
        
        // Optional fields
        // Assert.That(newRecord!.DateOfBirth == (requestDto.GetFieldOrDefault(XmlElementNames.Record.DateOfBirth)?.Value ?? requestDto.GetFieldOrDefault(XmlElementNames.Record.Id.DateOfBirth)?.Value ?? Constants.DefaultTableFieldValue.DateOfBirth));
        Assert.That(newRecord!.WebsiteUrl == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.WebsiteUrl)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.WebsiteUrl)?.Value ?? Constants.DefaultFieldValue.WebsiteUrl));
        Assert.That(newRecord!.PersonalEmail == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.PersonalEmail)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.EmailAddress)?.Value ?? Constants.DefaultFieldValue.PersonalEmail));
        Assert.That(newRecord!.Mobile == (expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Mobile)?.Value ?? expectedRecord.GetFieldOrDefault(XmlElementNames.Record.Id.Mobile)?.Value ?? Constants.DefaultFieldValue.Mobile));
        Assert.That(newRecord.VCardField == null);
    }
}