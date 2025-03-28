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

public class JsonQuickbaseApi  : IQuickbaseApi
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

    public Task<BaseResponseDto> AuthenticateAsync(AuthenticateRequestDto body)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponseDto> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRecordAsync(string tableId, string recordId)
    {
        throw new NotImplementedException();
    }
}