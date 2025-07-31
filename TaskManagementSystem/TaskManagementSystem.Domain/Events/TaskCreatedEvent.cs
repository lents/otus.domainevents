using MediatR;
using System;

namespace TaskManagementSystem.Domain.Events;

public class TaskCreatedEvent : INotification
{
    public Guid TaskId { get; }

    public TaskCreatedEvent(Guid taskId)
    {
        TaskId = taskId;
    }
}
