using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Events;
using System;

namespace TaskManagementSystem.Domain.Entities;

public class TaskDto : Entity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsDeleted { get; private set; }


    private TaskDto(Guid id, string title, string description, DateTime dueDate)
    {
        Id = id;
        Title = title;
        Description = description;
        IsCompleted = false;
        DueDate = dueDate;
        IsDeleted = false;
    }

    public static TaskDto Create(string title, string description, DateTime dueDate)
    {
        var task = new TaskDto(Guid.NewGuid(), title, description, dueDate);
        task.AddDomainEvent(new TaskCreatedEvent(task.Id));
        return task;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        AddDomainEvent(new TaskCompletedEvent(Id));
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        AddDomainEvent(new TaskDeletedEvent(Id));
    }
}
