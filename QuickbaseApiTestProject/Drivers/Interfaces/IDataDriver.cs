namespace QuickbaseApiTestProject.Drivers.Interfaces;

public interface IDataDriver
{
    public AuthenticateRequestDto AuthenticateRequest(Action<AuthenticateRequestDto>? setup = null);
}