using System.Text;
using System.Xml;

namespace QuickbaseApiTestProject.Utilities;

public static class XmlSerializeHelper
{
    private static XmlWriterSettings writerSettings = new XmlWriterSettings
    {
        Encoding = Encoding.UTF8,
        Indent = true
    };
    

    public static string SerializeToXml<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var memoryStream = new MemoryStream();
    
        using var xmlWriter = XmlWriter.Create(memoryStream, writerSettings);
        serializer.Serialize(xmlWriter, obj);
    
        memoryStream.Position = 0;
        using var streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        return streamReader.ReadToEnd();
    }

    public static T DeserializeFromXml<T>(string xml)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xml);
        var serializedObj = (T)serializer.Deserialize(reader);
        serializedObj.Should().NotBeNull($"Serialization failed for object of type {typeof(T)}");
        return serializedObj;
    }
}