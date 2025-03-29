using QuickbaseApiTestProject.Contracts;
using QuickbaseApiTestProject.DTOs.RequestDTOs;
using QuickbaseApiTestProject.DTOs.ResponseDTOs;

namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IQuickbaseApi
{
    Task<BaseResponse<AuthenticationResponseDto>> AuthenticateAsync(AuthenticateRequestDto body);
    Task<BaseResponse<AddRecordResponseDto>> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData);
    Task<bool> DeleteRecordAsync(string tableId, string recordId);
}

