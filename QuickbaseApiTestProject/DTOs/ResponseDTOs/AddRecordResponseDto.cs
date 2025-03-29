using System.Numerics;

namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

[XmlRoot("qdbapi")]
public record AddRecordResponseDto : BaseResponseDto
{
    /// <summary>
    /// Record ID of the record that was added
    /// </summary>
    [XmlElement("rid")]
    public int RecordId { get; set; }
    
    /// <summary>
    /// Used to detect update conflicts when invoking API_EditRecord.
    /// </summary>
    [XmlElement("updated_id")]
    public long UpdateId { get; set; }
}