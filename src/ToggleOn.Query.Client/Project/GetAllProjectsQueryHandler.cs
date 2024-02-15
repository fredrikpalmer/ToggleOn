using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Query.Client.Project;

internal class GetAllProjectsQueryHandler : IQueryHandler<GetAllProjectsQuery, IList<ProjectDto>>
{
    private readonly IProjectRepository _repository;

    public GetAllProjectsQueryHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProjectDto>> HandleAsync(GetAllProjectsQuery query, CancellationToken cancellationToken = default)
    {
        if(query is null) throw new ArgumentNullException(nameof(query));

        var projects = await _repository.GetAllAsync(cancellationToken);

        return projects.Select(p => new ProjectDto(p.Id, p.Name)).ToList();
    }
}
