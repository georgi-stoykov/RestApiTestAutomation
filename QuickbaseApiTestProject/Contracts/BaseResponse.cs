using System.Net;
using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject.Contracts;

public class BaseResponse<T> where T : class
{
    public readonly HttpStatusCode StatusCode;
    
    public readonly T Body;
    
    public BaseResponse(HttpResponseMessage message)
    {
        this.StatusCode = message.StatusCode;
        this.Body = ParseBody(message.Content);
    }

    private T ParseBody(HttpContent contentBody)
    {
        var content = contentBody.ReadAsStringAsync().GetAwaiter().GetResult();
        return XmlSerializeHelper.DeserializeFromXml<T>(content);
    }
}