using MediatR;
using System.Collections.Generic;
using TaskManagementSystem.Application.Features.Tasks.Dtos;

namespace TaskManagementSystem.Application.Features.Tasks.Queries;

public class GetAllTasksQuery : IRequest<IReadOnlyList<TaskDto>>
{
}
