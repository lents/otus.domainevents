using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Tasks.Dtos;

namespace TaskManagementSystem.Application.Features.Tasks.Queries;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task == null || task.IsDeleted)
        {
            return null; // Or throw a NotFoundException
        }

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            DueDate = task.DueDate
        };
    }
}
