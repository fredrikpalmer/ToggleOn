using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class FeatureGroupFilter : IFeatureFilter
{
    private readonly IFeatureEnricher _enricher;
    private readonly IFeatureGroupConfigurationAccessor _contextAccessor;

    public string Name => "ToggleOn.FeatureGroup";
    public bool IsShortCircuit => true;

    public FeatureGroupFilter(IFeatureEnricher enricher, IFeatureGroupConfigurationAccessor contextAccessor)
    {
        _enricher = enricher;
        _contextAccessor = contextAccessor;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        if(!context.Parameters.TryGetValue("FeatureGroups", out var featureGroups)) return Task.FromResult(false);

        var featureGroupNames = featureGroups.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var groupName in featureGroupNames)
        {
            var featureGroup = _contextAccessor.GetFeatureGroup(groupName);
            if (featureGroup is null) continue;

            var userId = _enricher.GetUserId();
            if (featureGroup.UserIds is not null && userId is not null && featureGroup.UserIds.Contains(userId)) return Task.FromResult(true);


            var ip = _enricher.GetIpAddress();
            if(featureGroup.IpAddresses is not null && ip is not null && featureGroup.IpAddresses.Contains(ip)) return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}
