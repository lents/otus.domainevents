using MediatR;
using System;

namespace TaskManagementSystem.Domain.Events;

public class TaskCompletedEvent : INotification
{
    public Guid TaskId { get; }

    public TaskCompletedEvent(Guid taskId)
    {
        TaskId = taskId;
    }
}
