using QuickbaseApiTestProject.Utilities;

namespace QuickbaseApiTestProject;

[SetUpFixture]
public class Hooks
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        TestServicesProvider.Initialize();
    }
}