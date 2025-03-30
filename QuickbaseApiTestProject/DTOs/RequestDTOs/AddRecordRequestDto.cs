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
    public List<KeyValuePair<string, FieldInfo>> Fields { get; set; } = new List<KeyValuePair<string, FieldInfo>>();

    [XmlElement(XmlElementNames.Field)]
    public List<Field> FieldList
    {
        get
        {
            var result = new List<Field>();
            foreach (var pair in Fields)
            {
                var field = new Field { Value = pair.Value.Value };

                // Set either Name or Fid based on the field info
                if (pair.Value.IsNameAttribute)
                {
                    field.Name = pair.Key;
                }
                else
                {
                    field.Fid = pair.Key;
                }

                result.Add(field);
            }

            return result;
        }
        set
        {
            Fields = new List<KeyValuePair<string, FieldInfo>>();
            if (value != null)
            {
                foreach (var field in value)
                {
                    string key = field.Name ?? field.Fid;
                    if (!string.IsNullOrEmpty(key))
                    {
                        Fields.Add(new KeyValuePair<string, FieldInfo>(key, new FieldInfo
                        {
                            Value = field.Value,
                            IsNameAttribute = !string.IsNullOrEmpty(field.Name)
                        }));
                    }
                }
            }
        }
    }
    
    public class Field
    {
        [XmlAttribute(XmlElementNames.Record.FieldAttribute.Name)]
        public string Name { get; set; }
        
        [XmlAttribute(XmlElementNames.Record.FieldAttribute.Fid)]
        public string Fid { get; set; }
        
        [XmlText]
        public string Value { get; set; }
    }
    
    public class FieldInfo
    {
        public string Value { get; set; }
        public bool IsNameAttribute { get; set; }
    }

    public void AddFieldAsName(string name, string value)
    {
        Fields.Add(new KeyValuePair<string, FieldInfo>(name, new AddRecordRequestDto.FieldInfo
        {
            Value = value,
            IsNameAttribute = true
        }));
    }

    public void AddFieldAsId(string fid, string value)
    {
        Fields.Add(new KeyValuePair<string, FieldInfo>(fid, new AddRecordRequestDto.FieldInfo
        {
            Value = value,
            IsNameAttribute = false
        }));
    }

    public FieldInfo GetField(string key)
    {
        return Fields.LastOrDefault(f => f.Key == key).Value;
    }

    public IEnumerable<FieldInfo> GetAllFields(string key)
    {
        return Fields.Where(f => f.Key == key).Select(f => f.Value);
    }

// Extension method to replace GetValueOrDefault used with Dictionary
    public FieldInfo GetFieldOrDefault(string key)
    {
        var pair = Fields.LastOrDefault(f => f.Key == key);
        return pair.Key == key ? pair.Value : null;
    }
}