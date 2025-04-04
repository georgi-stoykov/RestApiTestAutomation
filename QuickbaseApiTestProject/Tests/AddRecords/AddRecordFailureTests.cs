﻿using System.Net;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities.Constants;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
public class AddRecordFailureTests : RecordTests
{
    private string workEmailAsTestIdentifier;

    [SetUp]
    protected void TestSetup()
    {
        workEmailAsTestIdentifier = DataGenerator.NewEmail();
    }
    
    [Test(Description = "Record cannot be created without providing all mandatory field ids")]
    public async Task AddRecord_MissingMandatoryFieldIDs_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsId(XmlElementNames.Record.Id.FirstName, "TestFirstName");
        request.AddFieldAsId(XmlElementNames.Record.Id.Age, "25");
        request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, workEmailAsTestIdentifier);
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertFailedResponseProperties(response, Constants.ErrorCode.MissingRequiredField, Constants.ErrorText.MissingRequiredValue);
        await AssertRecordWasNotCreatedAsync();
    }

    [Test(Description = "Record cannot be created without providing all mandatory field names")]
    public async Task AddRecord_MissingMandatoryFieldNames_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, workEmailAsTestIdentifier);
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertFailedResponseProperties(response, Constants.ErrorCode.MissingRequiredField, Constants.ErrorText.MissingRequiredValue);
        await AssertRecordWasNotCreatedAsync();
    }
    
    [Test(Description = "Record cannot be created with field value exceeding its length limit")]
    public async Task AddRecord_ExceedFieldSize_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        
        //Maximum FirstName characters = 100
        var firstName =
            "AAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAABAAAAAAAAAB";
        request.AddFieldAsName(XmlElementNames.Record.FirstName, firstName);
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.WorkEmail, workEmailAsTestIdentifier);
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertFailedResponseProperties(response, Constants.ErrorCode.InvalidInput, Constants.ErrorText.InvalidInput, HttpStatusCode.BadRequest);
        await AssertRecordWasNotCreatedAsync();
    }
    
    [Test(Description = "When ignoring of errors is disabled, passing value to built-in fields fails the record creation request")]
    public async Task AddRecord_WriteInBuiltInField_DisabledErrorIgnore_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();

        var valueForBuildInField = 99999999;
        request.AddFieldAsId(XmlElementNames.Record.Id.BuildIn_RecordID, valueForBuildInField.ToString());
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertFailedResponseProperties(response, Constants.ErrorCode.CannotChangeValue, Constants.ErrorText.CannotChangeValue, HttpStatusCode.BadRequest);
        await AssertRecordWasNotCreatedAsync();
    }
    
    [Test(Description = "Passing invalid data type value for mandatory record field fails the record creation")]
    // ToDo: Ask if possible bug. The returned ErrorCode and ErrorText are strange. Not sure if the error code should be "2" or "50"
    public async Task AddRecord_InvalidDataForFieldType_MandatoryField_StrictValidation_ErrorResult()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "invalidNumber");
        request.AddFieldAsId(XmlElementNames.Record.Id.WorkEmail, workEmailAsTestIdentifier);

        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertFailedResponseProperties(response, Constants.ErrorCode.MissingRequiredField, Constants.ErrorText.MissingRequiredValue);
        await AssertRecordWasNotCreatedAsync();
    }
    
    [Test(Description = "Request for adding record without authentication tokens fails")]
    // ToDo: For some reason the authentication is working even without passing one or both of "Ticket" and "AppToken" values. These test can be split into multiple ones.
    public async Task AddRecord_Unauthorized_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest(useNameFields: false, includeOptionalFields: false);
        request.Ticket = null;
        // request.AppToken = null;
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertFailedResponseProperties(response, Constants.ErrorCode.InvalidApplicationToken, Constants.ErrorText.InvalidApplicationToken, HttpStatusCode.BadRequest);
        await AssertRecordWasNotCreatedAsync();
    }
    
    [Test(Description = "Record cannot be added field unique field value when another request with that value already exists in the table")]
    public async Task AddRecord_UniqueField_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.LastName, "TestLastName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        request.AddFieldAsName(XmlElementNames.Record.WorkEmail, workEmailAsTestIdentifier);
        var personalEmail1 = DataGenerator.NewEmail();
        request.AddFieldAsName(XmlElementNames.Record.PersonalEmail, personalEmail1);

        var response1 = await quickbaseApi.AddRecordAsync(tableId, request);
        await AssertRecordWasCreatedAsync(x => x.PersonalEmail == personalEmail1);

        var personalEmail2 = DataGenerator.NewEmail();
        request.GetField(XmlElementNames.Record.WorkEmail).Value = personalEmail2;
        var response2 = await quickbaseApi.AddRecordAsync(tableId, request);
        AssertFailedResponseProperties(response2, Constants.ErrorCode.UniqueFieldValueDuplication, Constants.ErrorText.UniqueFieldValueDuplication);
        await AssertRecordWasNotCreatedAsync(x => x.PersonalEmail == personalEmail2);
    }
    
    [Test(Description = "Request to create record for non-existent fails")]
    public async Task AddRecord_NonexistentTable_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest(useNameFields: false, includeOptionalFields: false);

        string nonExistentTableId = "a23456789";
        var response = await quickbaseApi.AddRecordAsync(nonExistentTableId, request);

        AssertFailedResponseProperties(response, Constants.ErrorCode.NoSuchDatabase, Constants.ErrorText.NoSuchDatabase);
        await AssertRecordWasNotCreatedAsync();
    }
    
    private void AssertFailedResponseProperties(
        BaseResponse<AddRecordResponseDto> response,
        int errorCode,
        string errorText,
        HttpStatusCode expectedStatus = HttpStatusCode.OK)
    {
        Assert.That(response.StatusCode == expectedStatus);
        Assert.That(response.Body.Action == ApiAction.API_AddRecord.ToString());
        Assert.That(response.Body.ErrorCode == errorCode);
        Assert.That(response.Body.ErrorText == errorText);
        Assert.That(response.Body.UserData == Constants.UserData);
        Assert.That(response.Body.RecordId == 0);
        Assert.That(response.Body.UpdateId == 0);
    }
    
    private async Task AssertRecordWasCreatedAsync(Func<TableRecord, bool>? uniqueRecordFilter = null)
    {
        uniqueRecordFilter = uniqueRecordFilter ?? (x => x.WorkEmail == workEmailAsTestIdentifier);
        var tableRecord = await GetTableRecordsAsync(uniqueRecordFilter);
        Assert.That(tableRecord != null, Constants.AssertionMessage.MissingExpectedRecord);
    }
    
    private async Task AssertRecordWasNotCreatedAsync(Func<TableRecord, bool>? uniqueRecordFilter = null)
    {
        uniqueRecordFilter = uniqueRecordFilter ?? (x => x.WorkEmail == workEmailAsTestIdentifier);
        var tableRecord = await GetTableRecordsAsync(uniqueRecordFilter);
        Assert.That(tableRecord == null, Constants.AssertionMessage.UnexpectedRecord);
    }
}