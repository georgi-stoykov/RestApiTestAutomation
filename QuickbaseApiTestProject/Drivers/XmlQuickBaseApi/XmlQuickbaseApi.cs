using System.Text;
using Microsoft.Extensions.Options;
using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.Drivers.Interfaces;
using QuickbaseApiTestProject.DTOs.RequestDTOs;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;
using QuickbaseApiTestProject.TestUtilities.Constants;
using QuickbaseApiTestProject.Utilities;
using QuickbaseApiTestProject.Utilities.ConfigDTOs;

namespace QuickbaseApiTestProject.Drivers.XmlQuickBaseApi;

public class XmlQuickbaseApi : IQuickbaseApi
{
    private readonly HttpClient httpClient;
    private readonly ApiSettingsConfig config;
    private readonly Encoding encodingType = Encoding.UTF8;
    private const string mediaType = "application/xml";

    public XmlQuickbaseApi(HttpClient httpClient, IOptionsMonitor<ApiSettingsConfig> settingsConfig)
    {
        this.httpClient = httpClient;
        config = settingsConfig.Get(ApiSettingsConfig.XmlApiConfig);
        this.httpClient.BaseAddress = new Uri(config.BaseApiUrl);
    }

    public async Task<BaseResponse<AuthenticationResponseDto>> AuthenticateAsync(AuthenticateRequestDto requestData)
    {
        string endpoint = string.Format(config.Endpoints.Authenticate, requestData);
        
        string xmlRequest = XmlSerializeHelper.SerializeToXml(requestData);
        
        var content = new StringContent(xmlRequest, encodingType, mediaType);
        content.Headers.Add(RequestHeaders.QuickBaseAction, nameof(ApiAction.API_Authenticate));
        
        var authResponse = await httpClient.PostAsync(endpoint, content);
        return new BaseResponse<AuthenticationResponseDto>(authResponse);
    }

    public async Task<BaseResponse<AddRecordResponseDto>> AddRecordAsync(string tableId, AddRecordRequestDto addRequest)
    {
        string endpoint = string.Format(config.Endpoints.Record, tableId);
        
        string xmlRequest = XmlSerializeHelper.SerializeToXml(addRequest);
        var content = new StringContent(xmlRequest, encodingType, mediaType);
        content.Headers.Add(RequestHeaders.QuickBaseAction, nameof(ApiAction.API_AddRecord));
        
        var addRecordResponse = await httpClient.PostAsync(endpoint, content);
        return new BaseResponse<AddRecordResponseDto>(addRecordResponse);
    }

    public async Task<BaseResponse<DoQueryResponseDto>> GetTableRecordsAsync(string tableId, DoQueryRequestDto doQueryRequestDto)
    {
        string endpoint = string.Format(config.Endpoints.Record, tableId);
        
        string xmlRequest = XmlSerializeHelper.SerializeToXml(doQueryRequestDto);
        var content = new StringContent(xmlRequest, encodingType, mediaType);
        content.Headers.Add(RequestHeaders.QuickBaseAction, nameof(ApiAction.API_DoQuery));
        
        var tableRecordsResponse = await httpClient.PostAsync(endpoint, content);
        return new BaseResponse<DoQueryResponseDto>(tableRecordsResponse);
    }
}