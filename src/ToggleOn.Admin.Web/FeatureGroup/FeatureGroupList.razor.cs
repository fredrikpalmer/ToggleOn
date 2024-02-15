
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureGroup;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Admin.Web.FeatureGroup;

public partial class FeatureGroupList
{
    private List<FeatureGroupViewModel>? _featureGroups;

    [Inject]
    IToggleOnQueryClient? QueryClient { get; set; }

    [Inject]
    IToggleOnCommandClient? CommandClient { get; set; }

    [Inject]
    IDialogService? DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var featureGroups = await QueryClient!.ExecuteAsync(new GetAllFeatureGroupsQuery());

        _featureGroups = featureGroups.Select(g => new FeatureGroupViewModel(g.Id, g.Name, g.UserIds, g.IpAddresses)).ToList();
    }

    async Task OnAddClick()
    {
        var parameters = new DialogParameters<AddFeatureGroupDialog>
        {
            { param => param.ButtonText, "Create" },
            { param => param.Color, Color.Primary }
        };

        var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var dialog = DialogService?.Show<AddFeatureGroupDialog>("Create", parameters, options);

        var dialogResult = await dialog?.Result!;
        if (!dialogResult.Cancelled)
        {
            var result = dialogResult.Data.As<CreateFeatureGroupViewModel>() ?? throw new InvalidOperationException();

            await CommandClient!.ExecuteAsync(new CreateFeatureGroupCommand(result.Id, result.Name, null, null));

            _featureGroups!.Add(new FeatureGroupViewModel(result.Id, result.Name, null, null));
        }
    }

    async Task OnEditClick(FeatureGroupViewModel item)
    {
        var parameters = new DialogParameters<EditFeatureGroupDialog>
        {
            { param => param.Model, new EditFeatureGroupViewModel
                {
                    Id = item.Id,
                    UserIds = item.UserIds,
                    IpAddresses = item.IpAddresses,
                }
            },
            { param => param.ButtonText, "Save" },
            { param => param.Color, Color.Primary }
        };

        var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var dialog = DialogService?.Show<EditFeatureGroupDialog>($"Edit {item.Name}", parameters, options);

        var dialogResult = await dialog?.Result!;
        if (!dialogResult.Cancelled)
        {
            var result = dialogResult.Data.As<EditFeatureGroupViewModel>() ?? throw new InvalidOperationException();

            await CommandClient!.ExecuteAsync(new UpdateFeatureGroupCommand(result.Id, result.UserIds, result.IpAddresses));

            var index = _featureGroups!.FindIndex(g => g.Id == result.Id);
            if (index > -1) _featureGroups[index] = new FeatureGroupViewModel(result.Id, item.Name, result.UserIds, result.IpAddresses);
        }
    }
}
