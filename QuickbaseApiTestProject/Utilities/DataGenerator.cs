namespace QuickbaseApiTestProject.Utilities;

public static class DataGenerator
{
    
    public static string AlphaNumberString()
    {
        return Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
    
    public static string NewEmail()
    {
        var name = AlphaNumberString();
        return $"{name}@mail.com";
    }
}