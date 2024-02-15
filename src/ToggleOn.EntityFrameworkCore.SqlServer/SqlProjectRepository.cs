using Microsoft.EntityFrameworkCore;
using ToggleOn.Data.Abstractions;
using ToggleOn.Domain;

namespace ToggleOn.EntityFrameworkCore.SqlServer;

internal class SqlProjectRepository : IProjectRepository
{
    private readonly ToggleOnContext _context;

    public SqlProjectRepository(ToggleOnContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects.ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task CreateAsync(Project project, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _context.Projects
            .Include(x => x.Features)
            .SingleAsync(p => p.Id == id, cancellationToken);

        if (project.Features.Any()) throw new InvalidOperationException("Deleting a project with feature toggles is not allowed");

        _context.Remove(project);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
