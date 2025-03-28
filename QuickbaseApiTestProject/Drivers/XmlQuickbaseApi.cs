using System.Text;
using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.TestUtilities;
using QuickbaseApiTestProject.TestUtilities.Constants;

namespace QuickbaseApiTestProject.Drivers;

public class XmlQuickbaseApi : IQuickbaseApi
{
    private readonly HttpClient httpClient;
    private readonly ApiSettingsConfig config;
    private string ticket; // For storing authentication ticket

    public XmlQuickbaseApi(HttpClient httpClient, IOptionsMonitor<ApiSettingsConfig> settingsConfig)
    {
        this.httpClient = httpClient;
        config = settingsConfig.Get(ApiSettingsConfig.XmlApiConfig);
        this.httpClient.BaseAddress = new Uri(config.BaseApiUrl);
    }

    public async Task<string> AuthenticateAsync(AuthenticateRequestDto body)
    {
        string endpoint = string.Format(config.Endpoints.Authenticate, body);
        
        string xmlRequest = SerializeToXml(body);
        var content = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
        content.Headers.Add("QUICKBASE-ACTION", nameof(ApiActionsEnum.API_Authenticate));
        
        var response = await httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        
        string xmlResponse = await response.Content.ReadAsStringAsync();
        
        // Parse XML response to extract ticket
        var authResponse = DeserializeFromXml<BaseResponseDto>(xmlResponse);
        ticket = authResponse.Ticket;
        
        return ticket;
    }

    public async Task<string> AddRecordAsync(string tableId, AddRecordRequestDto recordData)
    {
        string endpoint = string.Format(config.Endpoints.AddRecord, tableId);
        
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
        string endpoint = config.Endpoints.DeleteRecord;
        
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