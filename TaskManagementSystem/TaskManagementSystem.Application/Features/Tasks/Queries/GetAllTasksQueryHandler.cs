using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Tasks.Dtos;

namespace TaskManagementSystem.Application.Features.Tasks.Queries;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IReadOnlyList<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IReadOnlyList<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync();

        return tasks.Where(t => !t.IsDeleted).Select(task => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            DueDate = task.DueDate
        }).ToList();
    }
}
