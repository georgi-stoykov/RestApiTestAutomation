namespace QuickbaseApiTestProject.Utilities.Constants;

public abstract record Constants
{
    private const string emptyString = "";
    
    public const string UserData = "custom user data";

    public abstract record DefaultFieldValue
    {
        public const string DateOfBirth = emptyString;
        public const string Email = emptyString;
        public const string WebsiteUrl = "http://dummysite.com";
        public const string Mobile = emptyString;
    }
    
    // Assertion template
    public abstract record AssertionMessage
    {
        public const string RequestFailed = "Request failed with reason: \"{0}\"";
        public const string MissingExpectedRecord = "New record should have been added to the table.";
    }
    
    public abstract record ErrorCode
    {
        public const int NoError = 0;
        public const int InvalidInput = 2;
        public const int MissingRequiredField = 50;
        
    }
    
    public abstract record ErrorText
    {
        public const string NoError = "No error";
        public const string MissingRequiredValue = "Missing required value";
    }
}