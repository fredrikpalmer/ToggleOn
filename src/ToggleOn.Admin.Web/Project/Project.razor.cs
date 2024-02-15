using Microsoft.AspNetCore.Components;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Admin.Web.Project;

public partial class Project
{
    private ProjectViewModel? _project;

    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    IToggleOnQueryClient? QueryClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var project = await QueryClient!.ExecuteAsync(new GetProjectQuery(Id));

        _project = new(project.Id, project.Name);
    }
}
