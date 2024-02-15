using Microsoft.Extensions.DependencyInjection;

namespace ToggleOn.Abstractions;

public interface IToggleOnInProcessConfigurator 
{
    IServiceCollection Services { get; }
}
