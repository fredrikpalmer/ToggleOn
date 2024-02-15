using System.Security.Cryptography;
using System.Text;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class PercentageFilter : IFeatureFilter
{
    private readonly IFeatureEnricher _enricher;
    public string Name => "ToggleOn.Percentage";
    public bool IsShortCircuit => false;

    public PercentageFilter(IFeatureEnricher enricher)
    {
        _enricher = enricher;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        if (!context.Parameters.TryGetValue("Percentage", out var parameterValue)) return Task.FromResult(false);
        if (!int.TryParse(parameterValue, out var percentageRequirement)) return Task.FromResult(false);

        var ip = _enricher.GetIpAddress();

        using var hashAlgo = MD5.Create();
        var hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(ip));
        var number = BitConverter.ToInt32(hash, 0);
        var absolute = Math.Abs(number);

        var percentageValue = absolute % 100;
        return Task.FromResult(percentageValue <= percentageRequirement);
    }
}
