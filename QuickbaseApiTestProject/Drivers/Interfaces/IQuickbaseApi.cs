using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.RequestDTOs;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;

namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IQuickbaseApi
{
    Task<BaseResponse<AuthenticationResponseDto>> AuthenticateAsync(AuthenticateRequestDto requestData);
    Task<BaseResponse<AddRecordResponseDto>> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData);
    Task<BaseResponse<DoQueryResponseDto>> GetTableRecordsAsync(string tableId, DoQueryRequestDto doQueryRequestDto);
}

