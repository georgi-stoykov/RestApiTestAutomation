namespace QuickbaseApiTestProject.Utilities.Constants;

public abstract record XmlElementNames
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


    public abstract record Authentication
    {
        public const string Username = "username";
        public const string Password = "password";
        public const string Hours = "hours";
        public const string UserId = "userid";
    }
    
    public abstract record Database
    {
        public const string DatabaseInfo = "dbinfo";
        public const string Variables = "variables";
        public const string ChildDatabaseIds = "chdbids";
        public const string Name = "name";
        public const string Description = "desc";
    }

    public abstract record Record
    {
        public const string UpdateId = "update_id";
        public const string RecordId = "rid";
        public const string IncludeRecordId = "includeRids";
        public const string RecordAsString = "record";
        public const string IgnoreError = "ignoreError";
        
        public const string FirstName = "firstname";
        public const string LastName = "lastname";
        public const string Age = "age";
        public const string WorkEmail = "work_email";
        public const string DateOfBirth = "date_of_birth";
        public const string WebsiteUrl = "website_url";
        public const string PersonalEmail = "personal_email";
        public const string Mobile = "mobile";
        public const string VCardField = "vCardField";

        public abstract record Id
        {
            public const string FirstName = "6";
            public const string LastName = "7";
            public const string Age = "8";
            public const string WorkEmail = "14";
            public const string DateOfBirth = "9";
            public const string WebsiteUrl = "10";
            public const string EmailAddress = "11";
            public const string Mobile = "12";
            
            public const string BuildIn_RecordID = "3";
            public const string NonWritable_vCardField = "15";
        }
        public abstract record FieldAttribute
        {
            public const string Name = "name";
            public const string Fid = "fid";
        }
    }
}