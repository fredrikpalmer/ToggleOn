using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.Dialog;

public partial class DeleteDialog
{
    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    [Parameter] public string ContentText { get; set; } = string.Empty;

    [Parameter] public string ButtonText { get; set; } = string.Empty;

    [Parameter] public Color Color { get; set; } = Color.Default;

    [Parameter] public Guid ProjectId { get; set; }

    [Parameter] public EventCallback<Guid> OnDelete { get; set; }

    async Task Submit()
    {
        await OnDelete.InvokeAsync(ProjectId);
        MudDialog?.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog?.Cancel();
}
