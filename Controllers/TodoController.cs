using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAspnet.Data;
using WebAspnet.Models;

namespace WebAspnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return NotFound();
            return todo;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
        {
            todo.CreatedAt = DateTime.UtcNow;
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo todo)
        {
            if (id != todo.Id)
                return BadRequest();

            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return NotFound();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
