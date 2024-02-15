using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.FeatureGroup;

public partial class AddFeatureGroupDialog
{
    private CreateFeatureGroupViewModel _model = new();

    [CascadingParameter]
    public MudDialogInstance? Dialog { get; set; }

    [Parameter]
    public string ButtonText { get; set; }
    [Parameter]
    public Color Color { get; set; }

    void Submit() => Dialog?.Close(DialogResult.Ok(_model));
    void Cancel() => Dialog?.Cancel();
}
