namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

[XmlRoot(XmlElementNames.QdbApi)]
public record DoQueryResponseDto : BaseResponseDto
{
    [XmlElement(XmlElementNames.UserData)]
    public string UserData { get; set; }

    [XmlElement(XmlElementNames.Database.DatabaseInfo)]
    public DbInfo DatabaseInfo { get; set; }

    [XmlElement(XmlElementNames.Database.Variables)]
    public string Variables { get; set; }

    [XmlElement(XmlElementNames.Database.ChildDatabaseIds)]
    public string ChildDatabaseIds { get; set; }

    [XmlElement("record")] 
    public List<TableRecord> Records { get; set; }
}

public record DbInfo
{
    [XmlElement(XmlElementNames.Database.Name)]
    public string Name { get; set; }

    [XmlElement(XmlElementNames.Database.Description)]
    public string Description { get; set; }
}

public record TableRecord
{
    [XmlAttribute(XmlElementNames.Record.RecordId)] public int RecordId { get; set; }

    [XmlElement(XmlElementNames.Record.FirstName)] public string FirstName { get; set; }

    [XmlElement(XmlElementNames.Record.LastName)] public string LastName { get; set; }

    [XmlElement(XmlElementNames.Record.Age)] public int Age { get; set; }

    [XmlElement(XmlElementNames.Record.DateOfBirth)] public string DateOfBirth { get; set; }

    [XmlElement(XmlElementNames.Record.WebsiteUrl)] public string WebsiteUrl { get; set; }

    [XmlElement(XmlElementNames.Record.EmailAddress)] public string EmailAddress { get; set; }

    [XmlElement(XmlElementNames.Record.Mobile)] public string Mobile { get; set; }

    // Using the string property approach for BigInteger serialization
    [XmlIgnore] public BigInteger UpdateId { get; set; }

    [XmlElement(XmlElementNames.Record.UpdateId)]
    public string UpdateIdString
    {
        get => UpdateId.ToString();
        set => UpdateId = string.IsNullOrEmpty(value) ? BigInteger.Zero : BigInteger.Parse(value);
    }
}