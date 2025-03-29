namespace QuickbaseApiTestProject.DTOs;

[XmlRoot(XmlElementNames.QdbApi)]
public class AddRecordRequestDto
{
    [XmlElement(XmlElementNames.UserData)]
    public string UserData { get; set; }
    
    [XmlElement(XmlElementNames.Ticket)]
    public string Ticket { get; set; }
    
    [XmlElement(XmlElementNames.AppToken)]
    public string AppToken { get; set; }
    
    [XmlIgnore]
    public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    
    [XmlElement(XmlElementNames.Field)]
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
        [XmlAttribute(XmlElementNames.FieldKeyValue.Name)]
        public string Name { get; set; }
        
        [XmlText]
        public string Value { get; set; }
    }
}