using TaskManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface ITaskRepository
{
    Task<Task> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Task>> GetAllAsync();
    Task AddAsync(Task entity);
    Task UpdateAsync(Task entity);
    Task DeleteAsync(Task entity);
}
