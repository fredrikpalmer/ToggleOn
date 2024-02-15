using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

public class HttpContextEnricher : IFeatureEnricher
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextEnricher(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? GetIpAddress()
    {
        return _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
    }

    public string? GetUserId()
    {
        return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string? GetQueryString()
    {
        return _contextAccessor.HttpContext?.Request.QueryString.Value;
    }
}