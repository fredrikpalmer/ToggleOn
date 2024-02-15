using ToggleOn.Domain;

namespace ToggleOn.Data.Abstractions;

public interface IFeatureGroupRepository
{
    Task<IList<FeatureGroup>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FeatureGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(FeatureGroup featureGroup, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, string? userIds, string? ipAddresses, CancellationToken cancellationToken = default);
}
