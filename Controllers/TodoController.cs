using csi5112lec4b.models;
using csi5112lec4b.services;
using Microsoft.AspNetCore.Mvc;

namespace csi5112lec4b.controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase{
    private readonly TodoService _todoService;
    public TodoController(TodoService todoService) {
        this._todoService = todoService;
    }

    [HttpGet]
    public async Task<List<Todo>> Get() {
        return await _todoService.GetAsync();
    } 

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> Get(string Id) {
        var todo = await _todoService.GetAsync(Id);
        if (todo is null) {
            return NotFound();
        }
        return todo;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Todo newTodo) {
        await _todoService.CreateAsync(newTodo);
        return CreatedAtAction(nameof(Get), new {id = newTodo.Id}, newTodo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string Id, Todo updatedTodo) {
        bool updated = await _todoService.UpdateAsync(Id, updatedTodo);
        if (!updated) {
            // this assumes that a failed update is always caused by the object 
            // not being found. This needs to be changed if the cause may be different
            return NotFound();
        } 
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string Id) {
        var todo = await _todoService.GetAsync(Id);
        if (todo is null) {
            return NotFound();
        }
        await _todoService.DeleteAsync(todo.Id);
        return NoContent();
    }
}