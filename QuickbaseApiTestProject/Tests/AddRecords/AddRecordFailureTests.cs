namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
[TestFixture]
public class AddRecordFailureTests
{
    [Test()]
    public void AddRecord_MissingMandatoryFieldIDs_ReturnsError()
    {
        
    }
    
    
    [Test]
    public void AddRecord_MissingMandatoryFieldNames_ReturnsError()
    {
        
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
    public void AddRecord_InvalidDataForFieldType_MandatoryField_IntolerantValidation_ErrorResult()
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
}