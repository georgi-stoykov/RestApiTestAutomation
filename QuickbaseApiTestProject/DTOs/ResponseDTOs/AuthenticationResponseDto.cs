namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

[XmlRoot("qdbapi")]
public record AuthenticationResponseDto : BaseResponseDto
{
    /// <summary>
    /// Authentication ticket/token that can be used for subsequent API requests.
    /// </summary>
    [XmlElement("ticket")]
    public string Ticket { get; set; }
    
    
    /// <summary>
    /// Unique identifier for the authenticated user.
    /// </summary>
    [XmlElement("userid")]
    public string UserId { get; set; }
}