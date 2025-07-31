using MediatR;
using System;

namespace TaskManagementSystem.Application.Features.Tasks.Commands;

public class DeleteTaskCommand : IRequest
{
    public Guid Id { get; set; }
}
