using ToggleOn.Domain;

namespace ToggleOn.Data.Abstractions;

public interface IFeatureToggleRepository
{
    Task<IList<FeatureToggle>> GetAllAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IList<FeatureToggle>> GetAllAsync(Guid projectId, Guid? environmentId = null, CancellationToken cancellationToken = default);
    Task<IList<FeatureToggle>> GetAllAsync(string project, string environment, CancellationToken cancellationToken = default);
    Task<FeatureToggle> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Feature?> GetAsync(Guid projectId, string name, CancellationToken cancellationToken = default);
    Task CreateAsync(Feature feature, CancellationToken cancellationToken = default);
    Task CreateAsync(FeatureToggle featureToggle, CancellationToken cancellationToken = default);
    Task UpdateAsync(Feature feature, CancellationToken cancellationToken = default);   
    Task UpdateAsync(FeatureToggle featureToggle, CancellationToken cancellationToken = default);   
    Task UpdateEnabledAsync(Guid featureToggleId, bool enabled, CancellationToken cancellationToken);
    
}
