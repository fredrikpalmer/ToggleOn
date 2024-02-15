using Microsoft.Azure.SignalR.Management;

namespace ToggleOn.Functions;

public interface IHubContextStore
{
    ServiceHubContext? HubContext { get; }
}
