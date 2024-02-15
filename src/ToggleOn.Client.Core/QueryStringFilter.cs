using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class QueryStringFilter : IFeatureFilter
{
    private readonly IFeatureEnricher _enricher;

    public string Name => "ToggleOn.Querystring";

    public bool IsShortCircuit => true;

    public QueryStringFilter(IFeatureEnricher enricher)
    {
        _enricher = enricher;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        if (!context.Parameters.TryGetValue("key", out var key)) return Task.FromResult(false);
        if (!context.Parameters.TryGetValue("value", out var value)) return Task.FromResult(false);

        var queryString = _enricher.GetQueryString();
        if (queryString is null) return Task.FromResult(false);


        return Task.FromResult(queryString.Contains($"{key}={value}"));
    }
}
