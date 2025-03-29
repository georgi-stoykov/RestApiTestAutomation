namespace QuickbaseApiTestProject.DTOs.RequestDTOs;

[XmlRoot(XmlElementNames.QdbApi)]
public record AuthenticateRequestDto
{
    [XmlElement(XmlElementNames.Authentication.Username)]
    public required string Username { get; set; }
    
    [XmlElement(XmlElementNames.Authentication.Password)]
    public required string Password { get; set; }

    /// <summary>
    /// The number of hours the authentication token should remain valid
    /// </summary>
    [XmlElement(XmlElementNames.Authentication.Hours)]
    public int Hours { get; set; }

    /// <summary>
    /// Optional user data that can be included in the request
    /// </summary>
    [XmlElement(XmlElementNames.UserData)]
    public string UserData { get; set; }
}