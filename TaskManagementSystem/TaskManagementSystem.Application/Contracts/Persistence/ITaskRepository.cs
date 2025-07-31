using TaskManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface ITaskRepository
{
    Task<TaskDto> GetByIdAsync(Guid id);
    Task<IReadOnlyList<TaskDto>> GetAllAsync();
    Task AddAsync(TaskDto entity);
    Task UpdateAsync(TaskDto entity);
    Task DeleteAsync(TaskDto entity);
}
