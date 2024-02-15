using ToggleOn.Client.Abstractions;
using ToggleOn.Client.Core.Extensions;

namespace ToggleOn.Client.Core;

public class DefaultFeatureToggleEvaluator : IFeatureToggleEvaluator
{
    private readonly FeatureToggleEvaluatorOptions _options;
    private readonly IEnumerable<IFeatureFilter> _filters;

    public DefaultFeatureToggleEvaluator(FeatureToggleEvaluatorOptions options, IEnumerable<IFeatureFilter> filters)
    {
        _options = options;
        _filters = filters;
    }

    public async Task<bool> IsEnabledAsync(FeatureToggleEvaluationContext context, bool defaultValue = false)
    {
        try
        {
            if (!context.Filters.Any()) return context.Enabled;


            var filters = new List<(FeatureFilterEvaluationContext Context, IFeatureFilter Filter)>();
            foreach (var filterData in context.Filters)
            {
                var filter = _filters.FirstOrDefault(f => f.Name.Equals(filterData.Name, StringComparison.OrdinalIgnoreCase));
                if (filter is null)
                {
                    //TODO: Log missing filter

                    continue;
                }

                filters.Add((new() { Name = filterData.Name, Parameters = filterData.Parameters }, filter));
            }

            foreach (var (Context, Filter) in filters.Where(f => f.Filter.IsShortCircuit))
            {
                var enabled = await Filter.EvaluateAsync(Context).ConfigureAwait(false);
                if (enabled) return true;
            }

            if (context.Enabled == false) return false;

            if (_options.FilterRequirement == FilterRequirement.Any) return await filters.Where(f => !f.Filter.IsShortCircuit).Any(async f => await f.Filter.EvaluateAsync(f.Context).ConfigureAwait(false));

            return await filters.Where(f => !f.Filter.IsShortCircuit).All(async f => await f.Filter.EvaluateAsync(f.Context).ConfigureAwait(false));
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }
}
