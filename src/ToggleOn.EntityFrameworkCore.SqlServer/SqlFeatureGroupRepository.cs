using Microsoft.EntityFrameworkCore;
using ToggleOn.Data.Abstractions;
using ToggleOn.Domain;

namespace ToggleOn.EntityFrameworkCore.SqlServer;

internal class SqlFeatureGroupRepository : IFeatureGroupRepository
{
    private readonly ToggleOnContext _context;

    public SqlFeatureGroupRepository(ToggleOnContext context)
    {
        _context = context;
    }

    public async Task<IList<FeatureGroup>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.FeatureGroups.AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<FeatureGroup?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.FeatureGroups.AsNoTracking()
            .SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task CreateAsync(FeatureGroup featureGroup, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(featureGroup, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, string? userIds, string? ipAddresses, CancellationToken cancellationToken = default)
    {
        var featureGroup = await _context.FeatureGroups.SingleAsync(g => g.Id == id, cancellationToken);

        featureGroup.UserIds = userIds;
        featureGroup.IpAddresses = ipAddresses;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
