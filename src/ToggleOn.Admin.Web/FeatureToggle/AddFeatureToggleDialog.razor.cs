using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.FeatureToggle;

public partial class AddFeatureToggleDialog
{
    private CreateFeatureToggleViewModel model = new();

    [CascadingParameter]
    public MudDialogInstance? Dialog { get; set; }

    [Parameter]
    public string? ButtonText { get; set; }

    [Parameter]
    public Color Color { get; set; }

    void Submit() => Dialog?.Close(DialogResult.Ok(model));
    void Cancel() => Dialog?.Cancel();
}
