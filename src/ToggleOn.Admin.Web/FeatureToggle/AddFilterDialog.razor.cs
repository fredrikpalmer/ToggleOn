using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ToggleOn.Admin.Web.FeatureToggle;

public partial class AddFilterDialog
{
    private IList<AddFilterParameterViewModel> _parameters = new List<AddFilterParameterViewModel>();

    [CascadingParameter]
    public MudDialogInstance? Dialog { get; set; }

    [Parameter]
    public AddFilterViewModel Model { get; set; }

    [Parameter]
    public string? ButtonText { get; set; }

    [Parameter]
    public Color Color { get; set; }

    protected override void OnInitialized()
    {
        foreach (var parameter in Model.Parameters)
        {
            _parameters.Add(parameter);
        }

        _parameters.Add(new());
    }

    void CommittedItemChanges(AddFilterParameterViewModel item)
    {
        if (string.IsNullOrEmpty(item.Name)) return;
        if (string.IsNullOrEmpty(item.Value)) return;

        if(!_parameters.Any(p => string.IsNullOrEmpty(p.Name) && string.IsNullOrEmpty(p.Value))) _parameters.Add(new());
        
        if(!Model.Parameters.Any(p => p.Id == item.Id)) 
        {
            Model.Parameters.Add(item);
        }
        else
        {
            var index = Model.Parameters.FindIndex(p => p.Id == item.Id);
            if(index > -1) Model.Parameters[index] = item;
        }
    }

    void OnDeleteClick(AddFilterParameterViewModel item)
    {
        _parameters.Remove(item);

        if(Model.Parameters.Contains(item)) Model.Parameters.Remove(item);
    }

    void Submit() => Dialog?.Close(DialogResult.Ok(Model));

    void Cancel() => Dialog?.Cancel();
}
