namespace ToggleOn.Client.Abstractions;

public interface IFeatureEnricher
{
    string? GetUserId();

    string? GetIpAddress();

    string? GetQueryString();
}
