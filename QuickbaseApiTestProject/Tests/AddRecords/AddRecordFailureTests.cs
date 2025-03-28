namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
[TestFixture]
public class AddRecordFailureTests
{
    [Test()]
    public void AddRecord_MissingMandatoryFieldIDs_ReturnsError()
    {
        true.Should().BeTrue();
    }
    
    
    [Test]
    public void AddRecord_MissingMandatoryFieldNames_ReturnsError()
    {
        true.Should().BeTrue();
    }
    
    [Test]
    public void AddRecord_ExceedFieldIDSize_ReturnsError()
    {
        true.Should().BeTrue();
    }
    
    [Test]
    public void AddRecord_ExceedFieldNameSize_ReturnsError()
    {
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_WriteInBuiltInField_DisabledErrorIgnore_ReturnsError()
    {
        true.Should().BeTrue();
    }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_InvalidDataForFieldType_MandatoryField_IntolerantValidation_ErrorResult()
    {
        //test with phone number, no new record
        true.Should().BeTrue();
    }
    
    [Test()]
    public void AddRecord_Unauthorized_ReturnsError()
    {
        true.Should().BeTrue();
    }
    
    [Test()]
    public void AddRecord_UniqueField_ReturnsError()
    {
        true.Should().BeTrue();
    }
}