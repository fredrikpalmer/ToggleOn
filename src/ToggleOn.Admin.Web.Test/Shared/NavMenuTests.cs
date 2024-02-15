using ToggleOn.Admin.Web.Shared;
using Bunit;
using FluentAssertions;

namespace ToggleOn.Admin.Web.Tests.Shared;

public class NavMenuTests : IClassFixture<DefaultFixture>
{
    public DefaultFixture Fixture { get; }

    public NavMenuTests(DefaultFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public void Given_NavMenu_when_rendered_then_navLink_to_projects_page_should_exist()
    {
        var cut = Fixture.Context.RenderComponent<NavMenu>();

        cut.FindAll(".mud-nav-link-text").Select(c => c.TextContent).Should().Contain("Projects");
    }

    [Fact]
    public void Given_NavMenu_when_rendered_then_navLink_to_groups_page_should_exist()
    {
        var cut = Fixture.Context.RenderComponent<NavMenu>();

        cut.FindAll(".mud-nav-link-text").Select(c => c.TextContent).Should().Contain("Groups");
    }
}
