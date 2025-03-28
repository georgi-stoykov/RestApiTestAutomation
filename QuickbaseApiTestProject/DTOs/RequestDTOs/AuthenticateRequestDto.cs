namespace QuickbaseApiTestProject.DTOs;

public record AuthenticateRequestDto
{
    /// <summary>
    /// The username used for authentication
    /// </summary>
    [XmlElement("username")]
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    /// <summary>
    /// The password used for authentication
    /// </summary>
    [XmlElement("password")]
    [JsonPropertyName("password")]
    public required string Password { get; set; }

    /// <summary>
    /// The number of hours the authentication token should remain valid
    /// </summary>
    [XmlElement("hours")]
    [JsonPropertyName("hours")]
    public int Hours { get; set; }

    /// <summary>
    /// Optional user data that can be included in the request
    /// </summary>
    [XmlElement("udata")]
    [JsonPropertyName("udata")]
    public string UserData { get; set; }
}