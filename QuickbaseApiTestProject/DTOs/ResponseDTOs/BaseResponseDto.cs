﻿namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

[XmlRoot("qdbapi")]
public record BaseResponseDto
{
    /// <summary>
    /// The name of the API action that was performed.
    /// </summary>
    [XmlElement("action")]
    public string Action { get; set; }

    /// <summary>
    /// Error code returned by the API. 0 indicates success.
    /// </summary>
    [XmlElement("errcode")]
    public int ErrorCode { get; set; }

    /// <summary>
    /// Human-readable error message. "No error" when successful.
    /// </summary>
    [XmlElement("errtext")]
    public string ErrorText { get; set; }

    /// <summary>
    /// Optional user data that was included in the request and echoed back in the response.
    /// </summary>
    [XmlElement("udata")]
    public string UserData { get; set; }
}