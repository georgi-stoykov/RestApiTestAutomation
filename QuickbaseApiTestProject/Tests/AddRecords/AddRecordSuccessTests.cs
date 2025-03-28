namespace QuickbaseApiTestProject.Tests.AddRecords;

[Category("AddRecord")]
[TestFixture]
public class AddRecordSuccessTests
{
    // private TestContext testContext;
    //
    // [SetUp]
    // public void BeforeTest(IServiceProvider serviceProvider)
    // {
    //     this.testContext = serviceProvider.GetRequiredService<TestContext>();
    // }
    
    [Test(Description = "AAAAAAAAAAAA")]
    public void AddRecord_OnlyMandatoryFieldIDs_Successfully()
    {
        true.Should().BeTrue();
        // non-mandatory are set to their default values
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
}