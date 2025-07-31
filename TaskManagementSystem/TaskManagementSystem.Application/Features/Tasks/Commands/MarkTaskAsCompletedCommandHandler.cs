using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.Tasks.Commands;

public class MarkTaskAsCompletedCommandHandler : IRequestHandler<MarkTaskAsCompletedCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPublisher _publisher;

    public MarkTaskAsCompletedCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
    {
        _taskRepository = taskRepository;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(MarkTaskAsCompletedCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
        {
            // Or throw an exception
            return Unit.Value;
        }

        task.MarkAsCompleted();

        await _taskRepository.UpdateAsync(task);

        foreach (var domainEvent in task.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        task.ClearDomainEvents();

        return Unit.Value;
    }
}
