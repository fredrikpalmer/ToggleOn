namespace ToggleOn.Http;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ApiEndpointAttribute : Attribute
{
    public ApiEndpointAttribute(string template, params string[] parameterNames)
    {
        Template = template ?? throw new ArgumentNullException(nameof(template));
        ParameterNames = parameterNames;
    }

    public string Template { get; }
    public string[] ParameterNames { get; }
}
