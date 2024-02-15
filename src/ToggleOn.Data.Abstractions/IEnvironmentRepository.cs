namespace ToggleOn.Data.Abstractions;

public interface IEnvironmentRepository
{
    Task<IList<Domain.Environment>> GetAllAsync(Guid projectId, CancellationToken cancellationToken = default);

    Task CreateAsync(Domain.Environment environment, CancellationToken cancellationToken = default);
}
