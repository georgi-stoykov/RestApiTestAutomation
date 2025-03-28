namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IQuickbaseApi
{
    Task<BaseResponseDto> AuthenticateAsync(AuthenticateRequestDto body);
    Task<BaseResponseDto> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData);
    Task<bool> DeleteRecordAsync(string tableId, string recordId);
}

