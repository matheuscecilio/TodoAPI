using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Dtos;
using TodoAPI.Infra.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public TodoController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> Get() => await _context.Todos.ToListAsync();

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<TodoItem>> GetById(Guid id)
        => await _context.Todos.FindAsync(id) is TodoItem todo
            ? Ok(todo)
            : NotFound();

    [HttpPost]
    public async Task<IActionResult> Post(TodoItem todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return Created($"/todoitems/{todo.Id}", todo);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, TodoItem inputTodo)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo is null) return NotFound();

        todo.Description = inputTodo.Description;
        todo.IsComplete = inputTodo.IsComplete;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> Patch(Guid id, JsonPatchDocument<TodoItemPatchRequest> request)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo is null) return NotFound();

        var todoJsonPatchDocument = _mapper.Map<TodoItemPatchRequest>(todo);

        request.ApplyTo(todoJsonPatchDocument);

        _mapper.Map(todoJsonPatchDocument, todo);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (await _context.Todos.FindAsync(id) is TodoItem todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok(todo);
        }

        return NotFound();
    }
}
