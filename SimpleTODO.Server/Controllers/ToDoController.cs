using Microsoft.AspNetCore.Mvc;
using SimpleTODO.Models;
using SimpleTODO.Server.Logic;

namespace SimpleTODO.Controllers
{
    /// <summary>
    /// API controller for CRUD operations with TodoItems.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IToDoLogic _todoItemLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItemsController"/> class.
        /// </summary>
        /// <param name="todoItemLogic">The todo item logic.</param>
        public TodoItemsController(IToDoLogic todoItemLogic)
        {
            _todoItemLogic = todoItemLogic;
        }

        /// <summary>
        /// Retrieves all TodoItems.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoItemLogic.GetAllTodoItemsAsync();
            return Ok(todoItems);
        }

        /// <summary>
        /// Retrieves a specific TodoItem by its ID.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoItemLogic.GetTodoItemByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        /// <summary>
        /// Creates a new TodoItem.
        /// </summary>
        /// <param name="todoItem">The TodoItem to create.</param>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var newTodoItem = await _todoItemLogic.AddTodoItemAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = newTodoItem.Id }, newTodoItem);
        }

        /// <summary>
        /// Updates an existing TodoItem.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to update.</param>
        /// <param name="todoItem">The updated TodoItem.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            var result = await _todoItemLogic.UpdateTodoItemAsync(id, todoItem);
            if (!result)
            {
                return BadRequest();
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
            var result = await _todoItemLogic.DeleteTodoItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
