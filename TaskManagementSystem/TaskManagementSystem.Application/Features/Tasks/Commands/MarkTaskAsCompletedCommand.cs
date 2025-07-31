using MediatR;
using System;

namespace TaskManagementSystem.Application.Features.Tasks.Commands;

public class MarkTaskAsCompletedCommand : IRequest
{
    public Guid Id { get; set; }
}
