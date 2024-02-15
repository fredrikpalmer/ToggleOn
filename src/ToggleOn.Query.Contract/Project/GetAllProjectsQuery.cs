using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.Project;

[ApiEndpoint("projects")]
public class GetAllProjectsQuery : ToggleOnQuery<IList<ProjectDto>> { }