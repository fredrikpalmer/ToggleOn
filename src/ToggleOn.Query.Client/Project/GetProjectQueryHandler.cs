using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Query.Client.Project;

internal class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, ProjectDto>
{
    private readonly IProjectRepository _repository;

    public GetProjectQueryHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectDto> HandleAsync(GetProjectQuery query, CancellationToken cancellationToken = default)
    {
        if(query is null) throw new ArgumentNullException(nameof(query));

        var project = await _repository.GetAsync(query.Id, cancellationToken).ConfigureAwait(false);

        return new(project.Id, project.Name);
    }
}
