using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;
using ToggleOn.Data.Abstractions;

namespace ToggleOn.EntityFrameworkCore.SqlServer.Extensions;

public static class ToggleOnInProcessConfiguratorExtensions
{
    public static IToggleOnInProcessConfigurator UseSqlServer(this IToggleOnInProcessConfigurator configurator, string connectionString)
    {
        configurator.Services.AddDbContext<ToggleOnContext>(options => options.UseSqlServer(connectionString));

        configurator.Services.AddScoped<IProjectRepository, SqlProjectRepository>();
        configurator.Services.AddScoped<IEnvironmentRepository, SqlEnvironmentRepository>();
        configurator.Services.AddScoped<IFeatureToggleRepository, SqlFeatureToggleRepository>();
        configurator.Services.AddScoped<IFeatureGroupRepository, SqlFeatureGroupRepository>();

        return configurator;
    }

}