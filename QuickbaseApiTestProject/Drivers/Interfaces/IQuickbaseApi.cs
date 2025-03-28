namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IQuickbaseApi
{
    Task<string> AuthenticateAsync(AuthenticateRequestDto body);
    Task<string> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData);
    Task<bool> DeleteRecordAsync(string tableId, string recordId);
}

