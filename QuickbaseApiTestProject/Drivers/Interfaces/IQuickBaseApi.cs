namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IQuickBaseApi
{
    Task<string> AuthenticateAsync(string parameter);
    Task<string> AddRecordAsync(string tableId, object recordData);
    Task<bool> DeleteRecordAsync(string tableId, string recordId);
}

