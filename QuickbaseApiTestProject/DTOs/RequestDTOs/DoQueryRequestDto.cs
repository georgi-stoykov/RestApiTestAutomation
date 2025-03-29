namespace QuickbaseApiTestProject.DTOs.RequestDTOs;

[XmlRoot(XmlElementNames.QdbApi)]
public record DoQueryRequestDto
{
    [XmlElement(XmlElementNames.UserData)]
    public string UserData { get; set; }
    
    [XmlElement(XmlElementNames.Ticket)]
    public string Ticket { get; set; }
    
    [XmlElement(XmlElementNames.AppToken)]
    public string AppToken { get; set; }
    
    [XmlElement(XmlElementNames.Record.IncludeRecordId)]
    public int IncludeRecordId { get; set; }
}