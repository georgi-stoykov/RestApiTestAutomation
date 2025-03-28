namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IQuickBaseApi
{
    Task<string> AuthenticateAsync(AuthenticateRequestDto parameter);
    Task<string> AddRecordAsync(string tableId, AddRecordRequestDto recordRequestData);
    Task<bool> DeleteRecordAsync(string tableId, string recordId);
}

