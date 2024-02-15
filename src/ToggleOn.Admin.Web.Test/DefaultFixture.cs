using Moq;
using MudBlazor.Services;
using MudBlazor;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace ToggleOn.Admin.Web.Tests;

public class DefaultFixture
{
    public DefaultFixture()
    {
        BreakpointServiceMock = new Mock<IBreakpointService>();
        BrowserViewportServiceMock = new Mock<IBrowserViewportService>();
        ScrollManagerMock = new Mock<IScrollManager>();
        Context = new TestContext();

        Context.JSInterop.Mode = JSRuntimeMode.Loose;
        Context.Services.AddSingleton(BreakpointServiceMock.Object);
        Context.Services.AddSingleton(BrowserViewportServiceMock.Object);
        Context.Services.AddSingleton(ScrollManagerMock.Object);
    }

    public Mock<IBreakpointService> BreakpointServiceMock { get; }
    public Mock<IBrowserViewportService> BrowserViewportServiceMock { get; }
    public Mock<IScrollManager> ScrollManagerMock { get; }
    public TestContext Context { get; }
}
