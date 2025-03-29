namespace QuickbaseApiTestProject.DTOs.RequestDTOs;

[XmlRoot("qdbapi")]
public record AuthenticateRequestDto
{
    [XmlElement("username")]
    public required string Username { get; set; }
    
    [XmlElement("password")]
    public required string Password { get; set; }

    /// <summary>
    /// The number of hours the authentication token should remain valid
    /// </summary>
    [XmlElement("hours")]
    public int Hours { get; set; }

    /// <summary>
    /// Optional user data that can be included in the request
    /// </summary>
    [XmlElement("udata")]
    public string UserData { get; set; }
}