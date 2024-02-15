using FluentValidation;
using ToggleOn.Abstractions;
using ToggleOn.Command.Contract.Environment;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.Command.Client.Environment;

internal class CreateEnvironmentCommandHandler : ICommandHandler<CreateEnvironmentCommand, EnvironmentDto>
{
    private readonly IEnvironmentRepository _repository;
    private readonly IValidator<CreateEnvironmentCommand> _validator;
    private readonly IToggleOnEventPublisher _publisher;

    public CreateEnvironmentCommandHandler(IEnvironmentRepository repository, IValidator<CreateEnvironmentCommand> validator, IToggleOnEventPublisher publisher)
    {
        _repository = repository;
        _validator = validator;
        _publisher = publisher;
    }

    public async Task<EnvironmentDto> HandleAsync(CreateEnvironmentCommand command, CancellationToken cancellationToken = default)
    {
        if(command is null) throw new ArgumentNullException(nameof(command));

        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var environment = new Domain.Environment(command.Id, command.ProjectId, command.Name);
        await _repository.CreateAsync(environment, cancellationToken);

        await _publisher.Publish(new EnvironmentCreated(environment.ProjectId, environment.Id), cancellationToken);

        return new EnvironmentDto(environment.Id, environment.ProjectId, environment.Name);
    }
}
