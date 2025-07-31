using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Features.Tasks.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPublisher _publisher;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
    {
        _taskRepository = taskRepository;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = Domain.Entities.Task.Create(request.Title, request.Description, request.DueDate);

        await _taskRepository.AddAsync(task);

        foreach (var domainEvent in task.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        task.ClearDomainEvents();

        return task.Id;
    }
}
