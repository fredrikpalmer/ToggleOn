using System.Collections.Concurrent;
using ToggleOn.Client.Contract;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.AspNetCore.SignalR.Client;

internal class SignalRDataProvider : IToggleOnDataProvider, ISignalRInvocationHandler
{
    private readonly ConcurrentDictionary<string, FeatureToggleConfiguration> _featureToggles = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, FeatureGroupConfiguration> _featureGroups = new(StringComparer.OrdinalIgnoreCase);

    public event EventHandler? Initialized;

    public Task InitializeAsync(InitializeDto initialize)
    {
        foreach (var toggle in initialize.FeatureToggles)
        {
            _featureToggles.TryAdd(toggle.Name, new FeatureToggleConfiguration(
                toggle.Name,
                toggle.Enabled,
                toggle.Filters.Select(f => new FeatureFilterConfiguration(
                        f.Name,
                        new Dictionary<string, string>(f.Parameters, StringComparer.OrdinalIgnoreCase)
                    )).ToList()
                )
            );
        }

        foreach (var group in initialize.FeatureGroups)
        {
            _featureGroups.TryAdd(group.Name, new FeatureGroupConfiguration(group.Name, group.UserIds, group.IpAddresses));
        }

        //Initialized?.Invoke(this, EventArgs.Empty);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(FeatureToggleDto featureToggle)
    {
        var newToggle = new FeatureToggleConfiguration(
                featureToggle.Name,
                featureToggle.Enabled,
                featureToggle.Filters.Select(f => new FeatureFilterConfiguration(
                    f.Name,
                    new Dictionary<string, string>(f.Parameters, StringComparer.OrdinalIgnoreCase)
                ))
            .ToList()
        );

        _featureToggles.AddOrUpdate(
            featureToggle.Name,
            newToggle,
            (key, oldToggle) => key == oldToggle.Name ? newToggle : oldToggle);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(FeatureGroupDto featureGroup)
    {
        var newGroup = new FeatureGroupConfiguration(featureGroup.Name, featureGroup.UserIds, featureGroup.IpAddresses);

        _featureGroups.AddOrUpdate(featureGroup.Name, newGroup, (key, oldGroup) => key == oldGroup.Name ? newGroup : oldGroup);

        return Task.CompletedTask;
    }

    public FeatureToggleEvaluationContext? GetEvaluationContext(string name)
    {
        if (!_featureToggles.TryGetValue(name, out FeatureToggleConfiguration? toggle)) return null;

        return new FeatureToggleEvaluationContext { Name = toggle.Name, Enabled = toggle.Enabled, Filters = toggle.Filters };
    }

    public FeatureGroupConfiguration? GetFeatureGroupConfiguration(string name)
    {
        if (!_featureGroups.TryGetValue(name, out var config)) return null;

        return config;
    }
}