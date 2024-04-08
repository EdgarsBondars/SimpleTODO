    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SimpleTODO.Models;

    namespace SimpleTODO.Controllers
    {
        /// <summary>
        /// API controller for CRUD operations with TodoItems.
        /// </summary>
        [ApiController]
        [Route("api/[controller]")]
        public class TodoItemsController : ControllerBase
        {
            private readonly PriorityTaskDbContext _context;

            /// <summary>
            /// API controller for CRUD operations with TodoItems.
            /// </summary>
            /// <param name="context">The database context.</param>
            public TodoItemsController(PriorityTaskDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Retrieves all TodoItems.
            /// </summary>
            [HttpGet]
            public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
            {
                return await _context.TodoItems.ToListAsync();
            }

            /// <summary>
            /// Retrieves a specific TodoItem by its ID.
            /// </summary>
            /// <param name="id">The ID of the TodoItem to retrieve.</param>
            [HttpGet("{id}")]
            public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
            {
                var todoItem = await _context.TodoItems.FindAsync(id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                return todoItem;
            }

            /// <summary>
            /// Creates a new TodoItem.
            /// </summary>
            /// <param name="todoItem">The TodoItem to create.</param>
            [HttpPost]
            public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
            {
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            }

            /// <summary>
            /// Updates an existing TodoItem.
            /// </summary>
            /// <param name="id">The ID of the TodoItem to update.</param>
            /// <param name="todoItem">The updated TodoItem.</param>
            [HttpPut("{id}")]
            public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
            {
                if (id != todoItem.Id)
                {
                    return BadRequest();
                }

                _context.Entry(todoItem).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            /// <summary>
            /// Deletes a TodoItem by its ID.
            /// </summary>
            /// <param name="id">The ID of the TodoItem to delete.</param>
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTodoItem(int id)
            {
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null)
                {
                    return NotFound();
                }

                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool TodoItemExists(int id)
            {
                return _context.TodoItems.Any(e => e.Id == id);
            }
        }
    }