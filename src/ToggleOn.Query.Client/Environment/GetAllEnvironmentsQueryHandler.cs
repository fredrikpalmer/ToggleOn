using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.Environment;

namespace ToggleOn.Query.Client.Environment;

internal class GetAllEnvironmentsQueryHandler : IQueryHandler<GetAllEnvironmentsQuery, IList<EnvironmentDto>>
{
    private readonly IEnvironmentRepository _repository;

    public GetAllEnvironmentsQueryHandler(IEnvironmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<EnvironmentDto>> HandleAsync(GetAllEnvironmentsQuery query, CancellationToken cancellationToken = default)
    {
        if(query is null) throw new ArgumentNullException(nameof(query));

        var environments = await _repository.GetAllAsync(query.ProjectId, cancellationToken);

        return environments.Select(e => new EnvironmentDto(e.Id, e.ProjectId, e.Name)).ToList();
    }
}
