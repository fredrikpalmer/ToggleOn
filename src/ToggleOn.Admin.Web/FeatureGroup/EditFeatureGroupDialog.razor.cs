using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.FeatureGroup;

public partial class EditFeatureGroupDialog
{
    [CascadingParameter]
    public MudDialogInstance? Dialog { get; set; }

    [Parameter]
    public EditFeatureGroupViewModel Model { get; set; }

    [Parameter]
    public string ButtonText { get; set; }
    [Parameter]
    public Color Color { get; set; }

    void Submit() => Dialog?.Close(DialogResult.Ok(Model));
    void Cancel() => Dialog?.Cancel();
}
