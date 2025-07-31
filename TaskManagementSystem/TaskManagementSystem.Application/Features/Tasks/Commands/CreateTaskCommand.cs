using MediatR;
using System;

namespace TaskManagementSystem.Application.Features.Tasks.Commands;

public class CreateTaskCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}
