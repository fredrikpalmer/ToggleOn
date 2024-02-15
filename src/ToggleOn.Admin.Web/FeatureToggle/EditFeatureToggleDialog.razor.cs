using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;

namespace ToggleOn.Admin.Web.FeatureToggle
{
    public partial class EditFeatureToggleDialog
    {
        [CascadingParameter]
        public MudDialogInstance? Dialog { get; set; }

        [Parameter]
        public EditFeatureToggleViewModel Model { get; set; }

        [Parameter]
        public string? ButtonText { get; set; }

        [Parameter]
        public Color Color { get; set; }

        [Inject]
        IDialogService? DialogService { get; set; }

        async Task OnAddClick()
        {
            var parameters = new DialogParameters<AddFilterDialog>
            {
                { param => param.Model, new AddFilterViewModel { FeatureToggleId = Model.Id } },
                { param => param.ButtonText, "Add" },
                { param => param.Color, Color.Primary }
            };

            var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true };
            var dialog = DialogService?.Show<AddFilterDialog>("Add", parameters, options);
            if (dialog is null) return;

            var dialogResult = await dialog.Result;
            if(!dialogResult.Canceled)
            {
                var filter = dialogResult.Data.As<AddFilterViewModel>() ?? throw new InvalidOperationException();
                Model.Filters.Add(filter);
            }
        }

        async Task OnEditClick(AddFilterViewModel model)
        {
            var parameters = new DialogParameters<AddFilterDialog>
            {
                { param => param.Model, new AddFilterViewModel 
                    {
                        Id = model.Id,
                        FeatureToggleId = model.FeatureToggleId,
                        Name = model.Name,
                        Parameters = model.Parameters.Select(p => new AddFilterParameterViewModel
                        {
                            Id = p.Id,
                            FilterId = p.FilterId,
                            Name = p.Name,
                            Value = p.Value
                        }).ToList(),
                    } 
                },
                { param => param.ButtonText, "Save" },
                { param => param.Color, Color.Primary }
            };

            var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true };
            var dialog = DialogService?.Show<AddFilterDialog>($"Edit {model.Name}", parameters, options);
            if (dialog is null) return;

            var dialogResult = await dialog.Result;
            if (!dialogResult.Canceled)
            {
                var filter = dialogResult.Data.As<AddFilterViewModel>() ?? throw new InvalidOperationException();
               
                var filters = new List<AddFilterViewModel>();
                foreach (var item in Model.Filters)
                {
                    if (item.Id == filter.Id) filters.Add(filter);
                    else filters.Add(item);
                }

                Model.Filters = filters;
            }
        }

        void OnDeleteClick(AddFilterViewModel item)
        {
            Model.Filters.Remove(item);
        }

        void Submit() => Dialog?.Close(DialogResult.Ok(Model));
        void Cancel() => Dialog?.Cancel();

    }
}
