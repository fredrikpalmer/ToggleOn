using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Query.Client.Environment;
using ToggleOn.Query.Client.FeatureGroup;
using ToggleOn.Query.Client.FeatureToggle;
using ToggleOn.Query.Client.Project;
using ToggleOn.Query.Contract.Environment;
using ToggleOn.Query.Contract.FeatureGroup;
using ToggleOn.Query.Contract.FeatureToggle;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Query.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueryClient(this IServiceCollection services)
    {
        services.TryAddSingleton<IToggleOnQueryClient>((sp) => new DefaultToggleOnQueryClient(sp.GetRequiredService<ISendHandler>()));

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.TryAddScoped<IQueryHandler<GetAllProjectsQuery, IList<ProjectDto>>, GetAllProjectsQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetProjectQuery, ProjectDto>, GetProjectQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetAllEnvironmentsQuery, IList<EnvironmentDto>>, GetAllEnvironmentsQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetAllFeatureTogglesByIdQuery, IList<FeatureToggleDto>>, GetAllFeatureTogglesByIdQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetAllFeatureTogglesByNameQuery, IList<FeatureToggleDto>>, GetAllFeatureTogglesByNameQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetFeatureToggleQuery, FeatureToggleDto>, GetFeatureToggleQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetAllFeatureGroupsQuery, IList<FeatureGroupDto>>, GetAllFeatureGroupsQueryHandler>();
        services.TryAddScoped<IQueryHandler<GetFeatureGroupQuery, FeatureGroupDto?>, GetFeatureGroupQueryHandler>();

        return services;
    }
}
