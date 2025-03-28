namespace QuickbaseApiTestProject.DTOs;

[XmlRoot("qdbapi")]
public class AddRecordRequestDto
{
    [XmlElement("udata")]
    [JsonPropertyName("udata")]
    public string UserData { get; set; }
    
    [XmlElement("ticket")]
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; }
    
    [XmlElement("apptoken")]
    [JsonPropertyName("apptoken")]
    public string AppToken { get; set; }
    
    [XmlIgnore]
    [JsonIgnore]
    public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    
    [XmlElement("field")]
    public List<Field> FieldList
    {
        get
        {
            var result = new List<Field>();
            foreach (var pair in Fields)
            {
                result.Add(new Field { Name = pair.Key, Value = pair.Value });
            }
            return result;
        }
        set
        {
            Fields = new Dictionary<string, string>();
            if (value != null)
            {
                foreach (var field in value)
                {
                    Fields[field.Name] = field.Value;
                }
            }
        }
    }
    
    public class Field
    {
        [XmlAttribute("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [XmlText]
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}