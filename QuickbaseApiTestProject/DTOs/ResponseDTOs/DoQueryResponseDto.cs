namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

using System.Collections.Generic;
using System.Numerics;
using System.Xml.Serialization;

[XmlRoot("qdbapi")]
public record DoQueryResponseDto : BaseResponseDto
{
    [XmlElement("udata")] public string UserData { get; set; }

    [XmlElement("dbinfo")] public DbInfo DatabaseInfo { get; set; }

    [XmlElement("variables")] public string Variables { get; set; }

    [XmlElement("chdbids")] public string ChildDatabaseIds { get; set; }

    [XmlElement("record")] public List<TableRecord> Records { get; set; }
}

public record DbInfo
{
    [XmlElement("name")] public string Name { get; set; }

    [XmlElement("desc")] public string Description { get; set; }
}

public record TableRecord
{
    [XmlAttribute("rid")] public int RecordId { get; set; }

    [XmlElement("firstname")] public string FirstName { get; set; }

    [XmlElement("lastname")] public string LastName { get; set; }

    [XmlElement("age")] public int Age { get; set; }

    [XmlElement("date_of_birth")] public string DateOfBirth { get; set; }

    [XmlElement("website_url")] public string WebsiteUrl { get; set; }

    [XmlElement("email_address")] public string EmailAddress { get; set; }

    [XmlElement("mobile")] public string Mobile { get; set; }

    // Using the string property approach for BigInteger serialization
    [XmlIgnore] public BigInteger UpdateId { get; set; }

    [XmlElement("update_id")]
    public string UpdateIdString
    {
        get => UpdateId.ToString();
        set => UpdateId = string.IsNullOrEmpty(value) ? BigInteger.Zero : BigInteger.Parse(value);
    }
}