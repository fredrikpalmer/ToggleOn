using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.Project;

public partial class AddProjectDialog
{
    private CreateProjectViewModel model = new();

    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    [Parameter] public string ContentText { get; set; } = string.Empty;

    [Parameter] public string ButtonText { get; set; } = string.Empty;

    [Parameter] public Color Color { get; set; } = Color.Default;

    [Parameter] public EventCallback<ProjectViewModel> OnAdd { get; set; }

    async Task Submit()
    {
        await OnAdd.InvokeAsync(new ProjectViewModel(model.Id, model.Name));
        MudDialog?.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog?.Cancel();
}
