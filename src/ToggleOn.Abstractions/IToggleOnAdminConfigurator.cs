using Microsoft.Extensions.DependencyInjection;

namespace ToggleOn.Abstractions;

public interface IToggleOnAdminConfigurator
{
    IServiceCollection Services { get; }

    void AddHttpClient(string baseAddress);
    IToggleOnInProcessConfigurator AddInProcessClient();
}