namespace QuickbaseApiTestProject.DTOs.ResponseDTOs;

[XmlRoot(XmlElementNames.QdbApi)]
public record AddRecordResponseDto : BaseResponseDto
{
    /// <summary>
    /// Record ID of the record that was added
    /// </summary>
    [XmlElement(XmlElementNames.Record.RecordId)]
    public int RecordId { get; set; }
    
    /// <summary>
    /// Used to detect update conflicts when invoking API_EditRecord.
    /// </summary>
    [XmlElement(XmlElementNames.Record.UpdateId)]
    public long UpdateId { get; set; }
}