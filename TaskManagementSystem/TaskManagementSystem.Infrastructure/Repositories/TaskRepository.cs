using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<Task> _tasks = new();

    public Task<Task> GetByIdAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<IReadOnlyList<Task>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Task>>(_tasks);
    }

    public Task AddAsync(Task entity)
    {
        _tasks.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Task entity)
    {
        var index = _tasks.FindIndex(t => t.Id == entity.Id);
        if (index != -1)
        {
            _tasks[index] = entity;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Task entity)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == entity.Id);
        if (existingTask != null)
        {
            _tasks.Remove(existingTask);
        }
        return Task.CompletedTask;
    }
}
