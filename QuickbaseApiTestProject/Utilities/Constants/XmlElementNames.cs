namespace QuickbaseApiTestProject.Utilities.Constants;

public record XmlElementNames
{
    // Root element
    public const string QdbApi = "qdbapi";

    // Common elements
    public const string UserData = "udata";
    public const string Ticket = "ticket";
    public const string AppToken = "apptoken";
    public const string Field = "field";
    public const string Action = "action";
    public const string ErrorCode = "errcode";
    public const string ErrorText = "errtext";


    public record Authentication
    {
        public const string Username = "username";
        public const string Password = "password";
        public const string Hours = "hours";
        public const string UserId = "userid";
    }
    
    public record Database
    {
        public const string DatabaseInfo = "dbinfo";
        public const string Variables = "variables";
        public const string ChildDatabaseIds = "chdbids";
        public const string Name = "name";
        public const string Description = "desc";
    }

    public record Record
    {
        public const string UpdateId = "update_id";
        public const string RecordId = "rid";
        public const string IncludeRecordId = "includeRids";
        public const string RecordAsString = "record";
        
        public const string FirstName = "firstname";
        public const string LastName = "lastname";
        public const string Age = "age";
        public const string DateOfBirth = "date_of_birth";
        public const string WebsiteUrl = "website_url";
        public const string EmailAddress = "email_address";
        public const string Mobile = "mobile";

        public record Id
        {
            public const string FirstName = "6";
            public const string LastName = "7";
            public const string Age = "8";
            public const string DateOfBirth = "9";
            public const string WebsiteUrl = "10";
            public const string EmailAddress = "11";
            public const string Mobile = "12";
        }
        public record FieldAttribute
        {
            public const string Name = "name";
            public const string Fid = "fid";
        }
    }
}