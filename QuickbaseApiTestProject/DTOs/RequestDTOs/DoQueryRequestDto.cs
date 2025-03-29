namespace QuickbaseApiTestProject.DTOs.RequestDTOs;

[XmlRoot("qdbapi")]
public record DoQueryRequestDto
{
    [XmlElement("udata")]
    public string UserData { get; set; }
    
    [XmlElement("ticket")]
    public string Ticket { get; set; }
    
    [XmlElement("apptoken")]
    public string AppToken { get; set; }
    
    [XmlElement("includeRids")]
    public int IncludeRecordId { get; set; }
}