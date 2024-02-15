using Microsoft.AspNetCore.Components;
using ToggleOn.Admin.Web.FeatureToggle;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureToggle;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.Environment;

namespace ToggleOn.Admin.Web.Environment
{
    public partial class EnvironmentList
    {
        private IList<EnvironmentViewModel>? environments;
        private int selectedEnvIndex;

        [Inject]
        public IToggleOnQueryClient? QueryClient { get; set; }

        [Inject]
        public IToggleOnCommandClient? CommandClient { get; set; }

        [Parameter]
        public Guid ProjectId { get; set; }

        public EnvironmentViewModel? SelectedEnvironment => environments?[selectedEnvIndex];

        protected override async Task OnInitializedAsync()
        {
            var queryResult = await QueryClient!.ExecuteAsync(new GetAllEnvironmentsQuery(ProjectId));
            if (queryResult is null) return;

            environments = queryResult!
                .Select(e => new EnvironmentViewModel(e.Id, e.ProjectId, e.Name))
                .ToList();
        }

        void OnEnvAdded(EnvironmentViewModel environment)
        {
            if (environments is null) environments = new List<EnvironmentViewModel>();

            environments?.Add(environment);
        }
    }
}
