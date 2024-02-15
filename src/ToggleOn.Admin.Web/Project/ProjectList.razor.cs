using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToggleOn.Admin.Web.Dialog;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.Project;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Admin.Web.Project;

public partial class ProjectList
{
    private IList<ProjectViewModel>? projects;

    [Inject] IToggleOnQueryClient? QueryClient { get; set; }
    [Inject] IToggleOnCommandClient? CommandClient { get; set; }
    [Inject] NavigationManager? NavManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var queryResult = await QueryClient!.ExecuteAsync(new GetAllProjectsQuery());
        if (queryResult is null) return;

        projects = queryResult!.Select(q => new ProjectViewModel(q.Id, q.Name)).ToList();
    }

    void OnProjectClick(DataGridRowClickEventArgs<ProjectViewModel> e)
    {
        NavManager!.NavigateTo($"/projects/{e.Item.Id}");
    }

    void OnAddClick()
    {
        var parameters = new DialogParameters<AddProjectDialog>
        {
            { param => param.ButtonText, "Create" },
            { param => param.Color, Color.Primary },
            { param => param.OnAdd, new EventCallback<ProjectViewModel>(this, OnAddAsync)},

        };
        var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.ExtraSmall };
        DialogService!.Show<AddProjectDialog>("Create", parameters, options);
    }

    async Task OnAddAsync(ProjectViewModel viewModel)
    {
        if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));

        var command = new CreateProjectCommand(viewModel.Id, viewModel.Name!);
        await CommandClient!.ExecuteAsync(command);

        projects!.Add(new ProjectViewModel(viewModel.Id, viewModel.Name!));
    }

    void OnDeleteClick(Guid ProjectId)
    {
        var parameters = new DialogParameters<DeleteDialog>
        {
            { param => param.ContentText, "Do you really want to delete this project? This process cannot be undone." },
            { param => param.ButtonText, "Delete" },
            { param => param.Color, Color.Error },
            { param => param.ProjectId, ProjectId },
            { param => param.OnDelete, new EventCallback<Guid>(this, OnDeleteAsync) }
        };

        var options = new DialogOptions() { CloseButton = true, CloseOnEscapeKey = true, MaxWidth = MaxWidth.ExtraSmall };

        DialogService!.Show<DeleteDialog>("Delete", parameters, options);
    }

    async Task OnDeleteAsync(Guid projectId)
    {
        if(projectId == Guid.Empty) throw new ArgumentException("Invalid parameter value", nameof(projectId));

        var command = new DeleteProjectCommand(projectId);
        await CommandClient!.ExecuteAsync(command);

        projects = projects?.Where(p => p.Id != projectId).ToList();
    }
}
