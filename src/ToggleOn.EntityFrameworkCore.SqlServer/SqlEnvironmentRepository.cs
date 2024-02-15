using Microsoft.EntityFrameworkCore;
using ToggleOn.Data.Abstractions;

namespace ToggleOn.EntityFrameworkCore.SqlServer;

internal class SqlEnvironmentRepository : IEnvironmentRepository
{
    private readonly ToggleOnContext _context;

    public SqlEnvironmentRepository(ToggleOnContext context)
    {
        _context = context;
    }

    public async Task<IList<Domain.Environment>> GetAllAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.Environments
            .AsNoTracking()
            .Where(e => e.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(Domain.Environment environment, CancellationToken cancellationToken = default)
    {
        await _context.Environments.AddAsync(environment, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
