using System.Reflection;

namespace QuickbaseApiTestProject.Utilities;

public static class AttributeValueExtractor
{
    public static string GetXmlFieldName(Type type, string propertyName)
    {
        var property = type.GetProperty(propertyName);
        if (property == null)
            return propertyName;
        
        var xmlElementAttribute = property.GetCustomAttributes(typeof(XmlElementAttribute), true)
            .FirstOrDefault() as XmlElementAttribute;
        
        return xmlElementAttribute?.ElementName ?? propertyName;
    }
    
    public static string? GetXmlFieldName(this PropertyInfo propertyInfo)
    {
        if (propertyInfo == null)
        {
            return null;
        }

        var xmlElementAttr =
            propertyInfo.GetCustomAttribute<XmlElementAttribute>();

        if (
            xmlElementAttr != null
            && !string.IsNullOrWhiteSpace(xmlElementAttr.ElementName)
        )
        {
            return xmlElementAttr.ElementName;
        }

        return null;
    }
}