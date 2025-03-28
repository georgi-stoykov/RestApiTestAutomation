using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.TestUtilities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuickbaseApiTestProject.Drivers.Interfaces;

namespace QuickbaseApiTestProject.Drivers;

public class JsonQuickbaseApi 
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettingsConfig _config;
    private string _authToken; // For storing JWT token

    public JsonQuickbaseApi(HttpClient httpClient, IOptionsMonitor<ApiSettingsConfig> settingsConfig)
    {
        _httpClient = httpClient;
        _config = settingsConfig.Get(ApiSettingsConfig.JsonApiConfig);
        _httpClient.BaseAddress = new Uri(_config!.BaseApiUrl);
    }

    public async Task<string> AuthenticateAsync(AuthenticateRequestDto body)
    {
        string endpoint = string.Format(_config.Endpoints.Authenticate, body);
        
        // Create JSON request
        var authRequest = new
        {
            UserToken = body // Assuming parameter is userToken for REST API
        };
        
        var json = JsonSerializer.Serialize(authRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<BaseResponseDto>(jsonResponse);
        
        _authToken = authResponse.Ticket;
        
        // Set the auth token for future requests
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("QB-USER-TOKEN", _authToken);
        
        return _authToken;
    }

    public async Task<string> AddRecordAsync(string tableId, AddRecordRequestDto recordData)
    {
        var addRequest = new
        {
            to = tableId,
            data = new[] { recordData }
        };
        
        var json = JsonSerializer.Serialize(addRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(_config.Endpoints.AddRecord, content);
        response.EnsureSuccessStatusCode();
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        var addResponse = JsonSerializer.Deserialize<BaseResponseDto>(jsonResponse);
        
        // return addResponse.Metadata.CreatedRecordIds[0];
        return "";
    }

    public async Task<bool> DeleteRecordAsync(string tableId, string recordId)
    {
        var deleteRequest = new
        {
            from = tableId,
            where = $"{{3.EX.'{recordId}'}}" // Assuming record ID field is field 3
        };
        
        var json = JsonSerializer.Serialize(deleteRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(_config.Endpoints.DeleteRecord, content);
        return response.IsSuccessStatusCode;
    }
}