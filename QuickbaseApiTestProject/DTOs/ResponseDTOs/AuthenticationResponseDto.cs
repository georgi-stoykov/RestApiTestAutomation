namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

[XmlRoot(XmlElementNames.QdbApi)]
public record AuthenticationResponseDto : BaseResponseDto
{
    /// <summary>
    /// Authentication ticket/token that can be used for subsequent API requests.
    /// </summary>
    [XmlElement(XmlElementNames.Ticket)]
    public string Ticket { get; set; }
    
    /// <summary>
    /// Unique identifier for the authenticated user.
    /// </summary>
    [XmlElement(XmlElementNames.Authentication.UserId)]
    public string UserId { get; set; }
}