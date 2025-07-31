using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskDto> _tasks = new();

    public Task<TaskDto> GetByIdAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<IReadOnlyList<TaskDto>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<TaskDto>>(_tasks);
    }

    public Task AddAsync(TaskDto entity)
    {
        _tasks.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TaskDto entity)
    {
        var index = _tasks.FindIndex(t => t.Id == entity.Id);
        if (index != -1)
        {
            _tasks[index] = entity;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TaskDto entity)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == entity.Id);
        if (existingTask != null)
        {
            _tasks.Remove(existingTask);
        }
        return Task.CompletedTask;
    }
}
