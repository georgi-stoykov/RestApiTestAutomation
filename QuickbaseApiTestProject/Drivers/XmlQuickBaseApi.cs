using System.Text;
using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.TestUtilities;

namespace QuickbaseApiTestProject.Drivers;

public class XmlQuickBaseApi : IQuickBaseApi
{
    private readonly HttpClient _httpClient;
    private readonly IApiConfigProvider _config;
    private string _ticket; // For storing authentication ticket

    public XmlQuickBaseApi(HttpClient httpClient, IApiConfigProvider config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> AuthenticateAsync(string parameter)
    {
        string endpoint = string.Format(_config.Endpoints.Authenticate, parameter);
        
        // Create XML request
        var authRequest = new AuthenticateRequestDto
        {
            Username = parameter, // Assuming parameter is username
            Password = "your-password" // In a real app, this would come from secure storage
        };
        
        string xmlRequest = SerializeToXml(authRequest);
        var content = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
        
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        
        string xmlResponse = await response.Content.ReadAsStringAsync();
        
        // Parse XML response to extract ticket
        var authResponse = DeserializeFromXml<BaseResponseDto>(xmlResponse);
        _ticket = authResponse.Ticket;
        
        return _ticket;
    }

    public async Task<string> AddRecordAsync(string tableId, object recordData)
    {
        string endpoint = string.Format(_config.Endpoints.AddRecord, tableId);
        
        // // Create XML request with ticket and record data
        // var addRequest = new XmlAddRecordRequest
        // {
        //     Ticket = _ticket,
        //     TableId = tableId,
        //     Data = recordData
        // };
        //
        // string xmlRequest = SerializeToXml(addRequest);
        // var content = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
        //
        // var response = await _httpClient.PostAsync(endpoint, content);
        // response.EnsureSuccessStatusCode();
        //
        // string xmlResponse = await response.Content.ReadAsStringAsync();
        // var addResponse = DeserializeFromXml<XmlAddRecordResponse>(xmlResponse);
        //
        // return addResponse.RecordId;
        return "";
    }

    public async Task<bool> DeleteRecordAsync(string tableId, string recordId)
    {
        // Implementation for XML delete request
        string endpoint = _config.Endpoints.DeleteRecord;
        
        // var deleteRequest = new XmlDeleteRecordRequest
        // {
        //     Ticket = _ticket,
        //     TableId = tableId,
        //     RecordId = recordId
        // };
        //
        // string xmlRequest = SerializeToXml(deleteRequest);
        // var content = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
        //
        // var response = await _httpClient.PostAsync(endpoint, content);
        // return response.IsSuccessStatusCode;
        
        return true;
    }

    // Helper methods for XML serialization
    private string SerializeToXml<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var writer = new StringWriter();
        serializer.Serialize(writer, obj);
        return writer.ToString();
    }
    
    private T DeserializeFromXml<T>(string xml)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xml);
        return (T)serializer.Deserialize(reader);
    }
}