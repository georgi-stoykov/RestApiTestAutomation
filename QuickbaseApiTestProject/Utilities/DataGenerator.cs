namespace QuickbaseApiTestProject.Utilities;

public static class DataGenerator
{
    public static string Email()
    {
        var name =  Guid.NewGuid().ToString().Replace("-", string.Empty);
        return $"{name}@mail.com";
    }
}