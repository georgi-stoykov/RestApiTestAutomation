using QuickbaseApiTestProject.Drivers.Interfaces;

namespace QuickbaseApiTestProject.Drivers;

public class DataDriver : IDataDriver
{
    public AuthenticateRequestDto AuthenticateRequest(Action<AuthenticateRequestDto>? setup = null)
    {
        return new AuthenticateRequestDto
        {
            Username = null,
            Password = null
        };
    }
}