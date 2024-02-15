using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.Environment;

public partial class AddEnvironmentDialog
{
    private CreateEnvironmentViewModel model = new();

    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }
    [Parameter] public Guid ProjectId { get; set; }
    [Parameter] public string ContentText { get; set; } = string.Empty;

    [Parameter] public string ButtonText { get; set; } = string.Empty;

    [Parameter] public Color Color { get; set; } = Color.Default;

    [Parameter] public EventCallback<EnvironmentViewModel> OnAdd { get; set; }

    protected override void OnInitialized()
    {
        model.ProjectId = ProjectId;
    }

    async Task Submit()
    {
        await OnAdd.InvokeAsync(new EnvironmentViewModel(model.Id, model.ProjectId, model.Name!));
        MudDialog?.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog?.Cancel();
}
