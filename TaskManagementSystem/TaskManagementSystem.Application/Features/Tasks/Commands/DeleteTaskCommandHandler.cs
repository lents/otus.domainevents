using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.Tasks.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPublisher _publisher;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
    {
        _taskRepository = taskRepository;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
        {
            // Or throw an exception
            return Unit.Value;
        }

        task.MarkAsDeleted();

        await _taskRepository.UpdateAsync(task); // Or a specific DeleteAsync if we do a hard delete

        foreach (var domainEvent in task.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        task.ClearDomainEvents();

        return Unit.Value;
    }
}
