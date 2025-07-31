using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Features.Tasks.Commands;
using TaskManagementSystem.Application.Features.Tasks.Queries;
using TaskManagementSystem.Application.Features.Tasks.Dtos;
using System.Collections.Generic;


namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> GetAll()
    {
        var tasks = await _mediator.Send(new GetAllTasksQuery());
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetById(Guid id)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery { Id = id });
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
    {
        var taskId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = taskId }, command);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> MarkAsCompleted(Guid id)
    {
        await _mediator.Send(new MarkTaskAsCompletedCommand { Id = id });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTaskCommand { Id = id });
        return NoContent();
    }
}
