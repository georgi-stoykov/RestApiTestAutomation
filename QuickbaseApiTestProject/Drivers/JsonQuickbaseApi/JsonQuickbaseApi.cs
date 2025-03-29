using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.RequestDTOs;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities;

namespace QuickbaseApiTestProject.Drivers.JsonQuickbaseApi;

public class JsonQuickbaseApi
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettingsConfig _config;

    public JsonQuickbaseApi(HttpClient httpClient, IOptionsMonitor<ApiSettingsConfig> settingsConfig)
    {
        _httpClient = httpClient;
        _config = settingsConfig.Get(ApiSettingsConfig.JsonApiConfig);
        _httpClient.BaseAddress = new Uri(_config!.BaseApiUrl);
    }

    public Task<BaseResponse<AuthenticationResponseDto>> AuthenticateAsync(AuthenticateRequestDto body)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<BaseResponseDto>> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRecordAsync(string tableId, string recordId)
    {
        throw new NotImplementedException();
    }
}