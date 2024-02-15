using Microsoft.EntityFrameworkCore;
using ToggleOn.Domain;
using ToggleOn.Data.Abstractions;

namespace ToggleOn.EntityFrameworkCore.SqlServer;

internal class SqlFeatureToggleRepository : IFeatureToggleRepository
{
    private readonly ToggleOnContext _context;

    public SqlFeatureToggleRepository(ToggleOnContext context)
    {
        _context = context;
    }

    public Task<IList<FeatureToggle>> GetAllAsync(Guid projectId, CancellationToken cancellationToken) =>
        GetAllAsync(projectId, null, cancellationToken);

    public async Task<IList<FeatureToggle>> GetAllAsync(Guid projectId, Guid? environmentId = null, CancellationToken cancellationToken = default)
    {
        if(environmentId.HasValue) return await GetAllAsync(projectId, environmentId.Value, cancellationToken);

        return await _context.FeatureToggles.AsNoTracking()
            .Include(t => t.Feature)
            .Include(t => t.Filters)
            .ThenInclude(f => f.Parameters)
            .Where(t => t.Feature.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<FeatureToggle>> GetAllAsync(string project, string environment, CancellationToken cancellationToken = default)
    {
        return await _context.FeatureToggles.AsNoTracking()
            .Include(t => t.Feature)
            .Include(t => t.Filters)
            .ThenInclude(f => f.Parameters)
            .Join(_context.Projects, 
                t => t.Feature.ProjectId, 
                p => p.Id, 
                (t, p) => new 
                { 
                    Toggle = t, 
                    Project = p.Name 
                })
            .Join(_context.Environments, 
                t => t.Toggle.EnvironmentId, 
                e => e.Id, 
                (t, e) => new 
                {
                    t.Toggle,
                    t.Project,
                    Environment = e.Name 
                })
            .Where(t => t.Project == project && t.Environment == environment)
            .Select(t => t.Toggle)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<FeatureToggle> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.FeatureToggles.AsNoTracking()
            .Include(t => t.Feature)
            .ThenInclude(f => f.Project) //TODO: Remove include project
            .Include(t => t.Environment)
            .Include(t => t.Filters)
            .ThenInclude(f => f.Parameters)
            .SingleAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<Feature?> GetAsync(Guid projectId, string name, CancellationToken cancellationToken = default)
    {
        return await _context.Features.AsNoTracking()
            .Include(f => f.Toggles)
            .ThenInclude(t => t.Filters)
            .ThenInclude(f => f.Parameters)   
            .SingleOrDefaultAsync(f => f.ProjectId == projectId && f.Name == name, cancellationToken);
    }

    public async Task CreateAsync(Feature feature, CancellationToken cancellationToken = default)
    {
        await _context.Features.AddAsync(feature, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateAsync(FeatureToggle featureToggle, CancellationToken cancellationToken = default)
    {
        await _context.FeatureToggles.AddAsync(featureToggle, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Feature feature, CancellationToken cancellationToken = default)
    {
        _context.Update(feature);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FeatureToggle featureToggle, CancellationToken cancellationToken = default)
    {
        var toggle = await _context.FeatureToggles
            .Include(t => t.Filters)
            .ThenInclude(f => f.Parameters)
            .SingleAsync(t => t.Id == featureToggle.Id, cancellationToken);

        toggle.Enabled = featureToggle.Enabled;

        toggle.Filters.Clear();

        foreach (var filter in featureToggle.Filters)
        {
            toggle.Filters.Add(filter);
        }

        //foreach (var filter in featureToggle.Filters)
        //{
        //    var existingFilter = toggle.Filters.FirstOrDefault(t => t.Id == filter.Id);
        //    if (existingFilter is not null)
        //    {
        //        existingFilter.Name = filter.Name;

        //        foreach (var parameter in filter.Parameters)
        //        {
        //            var existingParameter = existingFilter.Parameters.FirstOrDefault(t => t.Id == parameter.Id);
        //            if (existingParameter is not null)
        //            {
        //                existingParameter.Name = parameter.Name;
        //                existingParameter.Value = parameter.Value;
        //            }
        //            else
        //            {
        //                existingFilter.Parameters.Add(new FeatureFilterParameter(parameter.Id, parameter.FeatureFilterId, parameter.Name, parameter.Value));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        toggle.Filters.Add(filter);
        //    }
        //}

        //var filters = toggle.Filters.ExceptBy(featureToggle.Filters.Select(f => f.Id), f => f.Id).ToList();
        //foreach (var filter in filters)
        //{
        //    toggle.Filters.Remove(filter);
        //}

        //var parameters = toggle.Filters.SelectMany(f => f.Parameters).ExceptBy(featureToggle.Filters.SelectMany(f => f.Parameters).Select(f => f.Id), f => f.Id).ToList();
        //foreach (var parameter in parameters)
        //{
        //    var filter = toggle.Filters.FirstOrDefault(f => f.Parameters.Contains(parameter));
        //    if (filter is null) continue;

        //    filter.Parameters.Remove(parameter);
        //}

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEnabledAsync(Guid featureToggleId, bool enabled, CancellationToken cancellationToken)
    {
        var toggle = await _context.Features
            .Include(f => f.Toggles)
            .SelectMany(f => f.Toggles)
            .SingleAsync(t => t.Id == featureToggleId, cancellationToken);

        toggle.Enabled = enabled;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<IList<FeatureToggle>> GetAllAsync(Guid projectId, Guid environmentId, CancellationToken cancellationToken = default)
    {
        return await _context.FeatureToggles
            .Include(t => t.Feature)
            .Include(t => t.Filters)
            .ThenInclude(f => f.Parameters)
            .Where(t => t.Feature.ProjectId == projectId && t.EnvironmentId == environmentId)
            .ToListAsync(cancellationToken);
    }
}
