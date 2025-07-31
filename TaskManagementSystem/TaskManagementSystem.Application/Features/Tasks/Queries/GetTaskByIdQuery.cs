using MediatR;
using System;
using TaskManagementSystem.Application.Features.Tasks.Dtos;

namespace TaskManagementSystem.Application.Features.Tasks.Queries;

public class GetTaskByIdQuery : IRequest<TaskDto>
{
    public Guid Id { get; set; }
}
