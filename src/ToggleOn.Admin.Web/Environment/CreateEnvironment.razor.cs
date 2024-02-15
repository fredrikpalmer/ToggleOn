using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.Environment;

namespace ToggleOn.Admin.Web.Environment;

public partial class CreateEnvironment
{
    [Inject]
    IDialogService? DialogService { get; set; }

    [Inject]
    IToggleOnCommandClient? CommandClient { get; set; }

    [Parameter]
    public Guid ProjectId { get; set; }

    [Parameter]
    public EventCallback<EnvironmentViewModel> OnAdded { get; set; }

    void OnAddClick()
    {
        var parameters = new DialogParameters<AddEnvironmentDialog>
        {
            { param => param.ButtonText, "Create" },
            { param => param.Color, Color.Primary },
            { param => param.ProjectId, ProjectId },
            { param => param.OnAdd, new EventCallback<EnvironmentViewModel>(this, OnAddAsync)},

        };
        var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.ExtraSmall };
        DialogService!.Show<AddEnvironmentDialog>("Create", parameters, options);
    }

    async Task OnAddAsync(EnvironmentViewModel viewModel)
    {
        await CommandClient!.ExecuteAsync(new CreateEnvironmentCommand(viewModel.Id, viewModel.ProjectId, viewModel.Name!));

        await OnAdded.InvokeAsync(viewModel);
    }
}
