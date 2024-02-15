using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToggleOn.Abstractions;
using ToggleOn.Command.Client.Environment;
using ToggleOn.Command.Client.FeatureGroup;
using ToggleOn.Command.Client.FeatureToggle;
using ToggleOn.Command.Client.Project;
using ToggleOn.Command.Contract;
using ToggleOn.Command.Contract.Environment;
using ToggleOn.Command.Contract.FeatureGroup;
using ToggleOn.Command.Contract.FeatureToggle;
using ToggleOn.Command.Contract.Project;
using ToggleOn.Contract;

namespace ToggleOn.Command.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandClient(this IServiceCollection services)
    {
        services.TryAddSingleton<IToggleOnCommandClient>((sp) => new DefaultToggleOnCommandClient(sp.GetRequiredService<ISendHandler>()));

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.TryAddScoped<ICommandHandler<CreateProjectCommand, ProjectDto>, CreateProjectCommandHandler>();
        services.TryAddScoped<ICommandHandler<DeleteProjectCommand, VoidResult>, DeleteProjectCommandHandler>();
        services.TryAddScoped<ICommandHandler<CreateEnvironmentCommand, EnvironmentDto>, CreateEnvironmentCommandHandler>();
        services.TryAddScoped<ICommandHandler<CreateFeatureToggleCommand, FeatureToggleDto>, CreateFeatureToggleCommandHandler>();
        services.TryAddScoped<ICommandHandler<UpdateFeatureToggleCommand, VoidResult>, UpdateFeatureToggleCommandHandler>();
        services.TryAddScoped<ICommandHandler<UpdateFeatureToggleTargetingCommand, VoidResult>, UpdateFeatureToggleTargetingCommandHandler>();
        services.TryAddScoped<ICommandHandler<CreateFeatureGroupCommand, VoidResult>, CreateFeatureGroupCommandHandler>();
        services.TryAddScoped<ICommandHandler<UpdateFeatureGroupCommand, VoidResult>, UpdateFeatureGroupCommandHandler>();

        services.TryAddScoped<IValidator<CreateProjectCommand>, CreateProjectCommandValidator>();
        services.TryAddScoped<IValidator<DeleteProjectCommand>, DeleteProjectCommandValidator>();
        services.TryAddScoped<IValidator<CreateEnvironmentCommand>, CreateEnvironmentCommandValidator>();
        services.TryAddScoped<IValidator<CreateFeatureGroupCommand>, CreateFeatureGroupCommandValidator>();

        return services;
    }
}
