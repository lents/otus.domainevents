using MediatR;
using System;

namespace TaskManagementSystem.Domain.Events;

public class TaskDeletedEvent : INotification
{
    public Guid TaskId { get; }

    public TaskDeletedEvent(Guid taskId)
    {
        TaskId = taskId;
    }
}
