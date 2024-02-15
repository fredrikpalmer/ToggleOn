using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ToggleOn.Admin.Web.Shared;

namespace ToggleOn.Admin.Web.Tests.Shared;

public class MainLayoutTest : IClassFixture<DefaultFixture>
{
    public DefaultFixture Fixture { get; }

    public MainLayoutTest(DefaultFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public void Given_MainLayout_when_rendered_then_correct_heading_should_exist()
    {
        //Arrange
        Fixture.Context.ComponentFactories.AddStub<MudThemeProvider>();
        Fixture.Context.ComponentFactories.AddStub<MudDialogProvider>();
        Fixture.Context.ComponentFactories.AddStub<MudSnackbarProvider>();

        //Act
        var cut = Fixture.Context.RenderComponent<MainLayout>();
        var headingElement = cut.Find("h6");

        //Assert
        headingElement.OuterHtml.Should().Be("<h6 class=\"mud-typography mud-typography-h6\">Dashboard</h6>");
    }

    [Fact]
    public void Given_MainLayout_when_rendered_then_drawer_should_be_open()
    {
        //Arrange
        Fixture.Context.ComponentFactories.AddStub<MudThemeProvider>();
        Fixture.Context.ComponentFactories.AddStub<MudDialogProvider>();
        Fixture.Context.ComponentFactories.AddStub<MudSnackbarProvider>();

        //Act
        var cut = Fixture.Context.RenderComponent<MainLayout>();

        //Assert
        cut.Find("aside").ClassList.Should().Contain("mud-drawer--open");
    }

    [Fact]
    public void Given_MainLayout_when_clicking_button_then_drawer_should_be_closed()
    {
        //Arrange
        Fixture.Context.ComponentFactories.AddStub<MudThemeProvider>();
        Fixture.Context.ComponentFactories.AddStub<MudDialogProvider>();
        Fixture.Context.ComponentFactories.AddStub<MudSnackbarProvider>();

        //Act
        var cut = Fixture.Context.RenderComponent<MainLayout>();
        cut.Find("button.mud-icon-button").TriggerEvent("onclick", new MouseEventArgs());

        //Assert
        cut.Find("aside").ClassList.Should().Contain("mud-drawer--closed");
    }
}