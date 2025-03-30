using System.Net;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities.Constants;

namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
[TestFixture]
public class AddRecordFailureTests : RecordTestHooks
{
    [Test]
    public async Task AddRecord_MissingMandatoryFieldIDs_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsId(XmlElementNames.Record.Id.FirstName, "TestFirstName");
        request.AddFieldAsId(XmlElementNames.Record.Id.Age, "25");
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertFailedResponseProperties(response, Constants.ErrorCode.MissingRequiredField, Constants.ErrorText.MissingRequiredValue);
        // ToDo: query the table by some unique field to assert it has not been added, e.g. "email_address"
    }
    
    
    [Test]
    public async Task AddRecord_MissingMandatoryFieldNames_ReturnsError()
    {
        var request = requestProvider.AddRecordRequest();
        request.Fields = new List<KeyValuePair<string, AddRecordRequestDto.FieldInfo>>();
        request.AddFieldAsName(XmlElementNames.Record.FirstName, "TestFirstName");
        request.AddFieldAsName(XmlElementNames.Record.Age, "25");
        
        var response = await quickbaseApi.AddRecordAsync(tableId, request);
        
        AssertFailedResponseProperties(response, Constants.ErrorCode.MissingRequiredField, Constants.ErrorText.MissingRequiredValue);
        // DoQuery by email 
    }
    
    [Test]
    public void AddRecord_ExceedFieldIDSize_ReturnsError()
    {
        
    }
    
    [Test]
    public void AddRecord_ExceedFieldNameSize_ReturnsError()
    {
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_WriteInBuiltInField_DisabledErrorIgnore_ReturnsError()
    {
        
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_InvalidDataForFieldType_MandatoryField_StrictValidation_ErrorResult()
    {
        //test with phone number, no new record
        
    }
    
    [Test()]
    public void AddRecord_Unauthorized_ReturnsError()
    {
        
    }
    
    [Test()]
    public void AddRecord_UniqueField_ReturnsError()
    {
        
    }
    
    [Test()]
    public void AddRecord_NonexistentTable_ReturnsError()
    {
        
    }
    
    private void AssertFailedResponseProperties(BaseResponse<AddRecordResponseDto> response, int errorCode, string errorText)
    {
        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(response.Body.Action == ApiAction.API_AddRecord.ToString());
        Assert.That(response.Body.ErrorCode == errorCode);
        Assert.That(response.Body.ErrorText == errorText);
        Assert.That(response.Body.UserData == Constants.UserData);
        Assert.That(response.Body.RecordId == 0);
        Assert.That(response.Body.UpdateId == 0);
    }
}