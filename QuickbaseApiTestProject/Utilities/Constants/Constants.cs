namespace QuickbaseApiTestProject.Utilities.Constants;

public abstract record Constants
{
    private const string emptyString = "";
    
    public const string UserData = "custom user data";

    public abstract record DefaultFieldValue
    {
        public const string DateOfBirth = emptyString;
        public const string PersonalEmail = emptyString;
        public const string WebsiteUrl = "http://dummysite.com";
        public const string Mobile = emptyString;
    }
    
    // Assertion template
    public abstract record AssertionMessage
    {
        public const string RequestFailed = "Request failed with reason: \"{0}\"";
        public const string MissingExpectedRecord = "New record should have been added to the table.";
        public const string UnexpectedRecord = "New record should have NOT been added to the table.";
    }
    
    public abstract record ErrorCode
    {
        public const int NoError = 0;
        public const int InvalidInput = 2;
        public const int InvalidApplicationToken = 24;
        public const int NoSuchDatabase = 32;
        public const int CannotChangeValue = 34;
        public const int MissingRequiredField = 50;
        public const int UniqueFieldValueDuplication = 51;
    }
    
    public abstract record ErrorText
    {
        public const string NoError = "No error";
        public const string InvalidInput = "Invalid input";
        public const string NoSuchDatabase = "No such database";
        public const string CannotChangeValue = "You cannot change the value of this field";
        public const string InvalidApplicationToken = "Invalid Application Token";
        public const string MissingRequiredValue = "Missing required value";
        public const string UniqueFieldValueDuplication = "Trying to add a non-unique value to a field marked unique";
    }
}