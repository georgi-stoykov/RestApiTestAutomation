namespace QuickbaseApiTestProject.DTOs;

public record BaseResponseDto
{
    /// <summary>
    /// The name of the API action that was performed.
    /// </summary>
    [XmlElement("action")]
    [JsonPropertyName("action")]
    public string Action { get; set; }

    /// <summary>
    /// Error code returned by the API. 0 indicates success.
    /// </summary>
    [XmlElement("errcode")]
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }

    /// <summary>
    /// Human-readable error message. "No error" when successful.
    /// </summary>
    [XmlElement("errtext")]
    [JsonPropertyName("errtext")]
    public string ErrorText { get; set; }

    /// <summary>
    /// Optional user data that was included in the request and echoed back in the response.
    /// </summary>
    [XmlElement("udata")]
    [JsonPropertyName("udata")]
    public string UserData { get; set; }

    /// <summary>
    /// Authentication ticket/token that can be used for subsequent API requests.
    /// </summary>
    [XmlElement("ticket")]
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; }

    /// <summary>
    /// Unique identifier for the authenticated user.
    /// </summary>
    [XmlElement("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }
}