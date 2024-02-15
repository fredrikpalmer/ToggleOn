using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using ToggleOn.Admin.Web.Environment;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureToggle;
using ToggleOn.Contract;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Admin.Web.FeatureToggle;

public partial class FeatureToggleList
{
    private List<FeatureToggleViewModel>? featureToggles;

    [Inject]
    public IToggleOnQueryClient? QueryClient { get; set; }

    [Inject]
    public IToggleOnCommandClient? CommandClient { get; set; }

    [Inject]
    public IDialogService? DialogService { get; set; }

    [Parameter]
    public Guid ProjectId { get; set; }

    [Parameter]
    public Guid EnvironmentId { get; set; }

    [Parameter]
    public IList<EnvironmentViewModel> Environments { get; set; } = new List<EnvironmentViewModel>();

    protected override async Task OnInitializedAsync()
    {
        var queryResult = await QueryClient!.ExecuteAsync(new GetAllFeatureTogglesByIdQuery(ProjectId, EnvironmentId));

        featureToggles = queryResult!.Select(t => new FeatureToggleViewModel(
            t.Id,
            t.FeatureId,
            t.EnvironmentId,
            t.Name,
            t.Enabled,
            t.Filters.Select(f => new FilterViewModel(
                f.Id,
                f.FeatureToggleId,
                f.Name,
                f.Parameters.Select(p => new FilterParameterViewModel(
                    p.Id,
                    p.FeatureFilterId,
                    p.Name,
                    p.Value))
                .ToList()))
            .ToList()))
        .ToList();
    }

    async Task OnAddClick()
    {
        var parameters = new DialogParameters<AddFeatureToggleDialog>
        {
            { param => param.ButtonText, "Create" },
            { param => param.Color, Color.Primary }
        };

        var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var dialog = DialogService?.Show<AddFeatureToggleDialog>("Create", parameters, options);

        var result = await dialog?.Result!;
        if(!result.Cancelled)
        {
            var viewModel = result.Data.As<CreateFeatureToggleViewModel>() ?? throw new InvalidOperationException();

            var featureToggle = await CommandClient!.ExecuteAsync(new CreateFeatureToggleCommand(viewModel.Id, viewModel.FeatureId, ProjectId, EnvironmentId, viewModel.Name, false));

            featureToggles!.Add(new FeatureToggleViewModel(
                featureToggle.Id, 
                featureToggle.FeatureId, 
                featureToggle.EnvironmentId, 
                featureToggle.Name, 
                featureToggle.Enabled, 
                featureToggle.Filters.Select(f => new FilterViewModel( 
                        f.Id, 
                        f.FeatureToggleId, 
                        f.Name, 
                        f.Parameters.Select(p => new FilterParameterViewModel(
                                p.Id, 
                                p.FeatureFilterId, 
                                p.Name, 
                                p.Value))
                        .ToList()))
                .ToList())
            );
        }
    }

    async Task OnEditClick(FeatureToggleViewModel viewModel)
    {
        var parameters = new DialogParameters<EditFeatureToggleDialog>
        {
            { param => param.Model, new EditFeatureToggleViewModel 
                { 
                    Id = viewModel.Id, 
                    FeatureId = viewModel.FeatureId,
                    EnvironmentId = viewModel.EnvironmentId,
                    Name = viewModel.Name, 
                    Enabled = viewModel.Enabled, 
                    Filters = viewModel.Filters.Select(f => new AddFilterViewModel
                    {
                        Id = f.Id,
                        FeatureToggleId = f.FeatureToggleId,
                        Name = f.Name,
                        Parameters = f.Parameters.Select(p => new AddFilterParameterViewModel
                        {
                            Id = p.Id,
                            FilterId = p.FilterId,
                            Name = p.Name,
                            Value = p.Value
                        }).ToList()
                    }).ToList()   
                } 
            },
            { param => param.ButtonText, "Save" },
            { param => param.Color, Color.Primary }
        };

        var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var dialog = DialogService?.Show<EditFeatureToggleDialog>($"Edit {viewModel.Name}", parameters, options);

        var dialogResult = await dialog?.Result!;
        if (!dialogResult.Cancelled)
        {
            var editResult = dialogResult.Data.As<EditFeatureToggleViewModel>() ?? throw new InvalidOperationException();

            await CommandClient!.ExecuteAsync(new UpdateFeatureToggleCommand(
                editResult.Id, 
                editResult.FeatureId, 
                editResult.EnvironmentId,
                editResult.Enabled, 
                editResult.Filters.Select(f => new FeatureFilterDto(
                    f.Id, 
                    f.FeatureToggleId, 
                    f.Name, 
                    f.Parameters.Select(p => new FeatureFilterParameterDto(
                        p.Id, 
                        p.FilterId, 
                        p.Name, 
                        p.Value))
                    .ToHashSet()))
                .ToList()
            ));

            var index = featureToggles!.FindIndex(t => t.Id == editResult.Id);
            if (index > -1) featureToggles[index] = new FeatureToggleViewModel(
                editResult.Id,
                editResult.FeatureId,
                editResult.EnvironmentId,
                editResult.Name,
                editResult.Enabled,
                editResult.Filters.Select(f => new FilterViewModel(
                    f.Id,
                    f.FeatureToggleId,
                    f.Name,
                    f.Parameters.Select(p => new FilterParameterViewModel(
                        p.Id,
                        p.FilterId,
                        p.Name,
                        p.Value))
                    .ToList()))
                .ToList());
        }
    }

    async Task OnUpdateFeatureToggleEnabled(FeatureToggleViewModel viewModel, bool enabled)
    {
        await CommandClient!.ExecuteAsync(new UpdateFeatureToggleTargetingCommand(viewModel.Id, enabled));

        viewModel.Enabled = enabled;
    }
}

