namespace ToggleOn.AspNetCore.SignalR.Client;

public interface ITokenGenerator
{
    string Generate(string audience, string accessKey);
}
