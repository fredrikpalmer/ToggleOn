using ToggleOn.Abstractions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ToggleOn.Http;

namespace ToggleOn.Core;

internal class HttpSendHandler : ISendHandler
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public HttpSendHandler(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    }

    public async Task<TResult> HandleAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        if (query is null) throw new ArgumentNullException(nameof(query));

        string endpoint = GetApiEndpoint(query);

        //var endpoint = query.GetType().Name.ToLower().Replace("query", "");

        try
        {
            var response = await _httpClient.GetAsync($"{endpoint}{GetQueryString(query)}", cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return (await JsonSerializer.DeserializeAsync<TResult>(stream, _serializerOptions, cancellationToken))!;
        }
        catch (Exception ex)
        {
            //TODO: Log exception
            throw;
        }
    }

    public async Task<TResult> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if(command is null) throw new ArgumentNullException(nameof(command));

        var endpoint = GetApiEndpoint(command);
        var httpMethod = GetHttpMethod(command);

        try
        {
            return httpMethod switch
            {
                "PUT" => await PutAsync(command, endpoint, cancellationToken),
                "PATCH" => await PatchAsync(command, endpoint, cancellationToken),
                "DELETE" => await DeleteAsync(command, endpoint, cancellationToken),
                _ => await PostAsync(command, endpoint, cancellationToken)
            };
        }
        catch (Exception)
        {
            //TODO: Log exception
            throw;
        }
    }

    private async Task<TResult> PostAsync<TResult>(ICommand<TResult> command, string endpoint, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(command, command.GetType(), _serializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return (await JsonSerializer.DeserializeAsync<TResult>(stream, _serializerOptions, cancellationToken))!;
    }
    private async Task<TResult> PutAsync<TResult>(ICommand<TResult> command, string endpoint, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(command, command.GetType(), _serializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(endpoint, content, cancellationToken);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return (await JsonSerializer.DeserializeAsync<TResult>(stream, _serializerOptions, cancellationToken))!;
    }

    private async Task<TResult> PatchAsync<TResult>(ICommand<TResult> command, string endpoint, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(command, command.GetType(), _serializerOptions);
        var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");


        var response = await _httpClient.PatchAsync(endpoint, content, cancellationToken);

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return (await JsonSerializer.DeserializeAsync<TResult>(stream, _serializerOptions, cancellationToken))!;
    }

    private async Task<TResult> DeleteAsync<TResult>(ICommand<TResult> command, string endpoint, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{endpoint}{GetQueryString(command)}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return (await JsonSerializer.DeserializeAsync<TResult>(stream, _serializerOptions, cancellationToken))!;
    }

    private static string? GetHttpMethod(object obj)
    {
        var attribute = obj.GetType().GetCustomAttribute<UseHttpMethodAttribute>();

        return attribute?.HttpMethod;
    }

    private static string GetApiEndpoint(object obj)
    {
        var route = obj.GetType().GetCustomAttribute<ApiEndpointAttribute>();
        if (route?.Template is null) throw new NotImplementedException($"{obj.GetType().Name} should implement {nameof(ApiEndpointAttribute)}");

        return string.Format(route.Template, route.ParameterNames
            .Select(p => obj.GetType().GetProperty(p))
            .Where(p => p is not null)
            .Select(p => p!.GetValue(obj))
            .Where(p => p is not null)
            .Select(p => p!.ToString()).ToArray());
    }

    private static string GetQueryString(object obj)
    {
        var properties = obj.GetType().GetProperties();
        if (!properties.Any()) return string.Empty;

        var queryString = properties
            .Select(p => $"{p.Name}={p.GetValue(obj)}")
            .Aggregate((q1, q2) => $"{q1}&{q2}");

        return $"?{queryString}";
    }
}