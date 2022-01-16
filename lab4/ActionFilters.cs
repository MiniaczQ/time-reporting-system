namespace lab4.ActionFilters
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class ReqLoggedIn : Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.All)]
    public class ReqLoggedOut : Attribute
    {
    }
}