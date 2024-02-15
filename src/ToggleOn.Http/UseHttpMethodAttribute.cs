namespace ToggleOn.Http;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class UseHttpMethodAttribute : Attribute
{
    public UseHttpMethodAttribute(string httpMethod)
    {
        HttpMethod = httpMethod;
    }

    public string HttpMethod { get; }

}
