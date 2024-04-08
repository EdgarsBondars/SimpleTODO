using Microsoft.EntityFrameworkCore;
using SimpleTODO.Models;

namespace SimpleTODO.Server.Logic
{
    /// <summary>
    /// Provides logic for managing TodoItems.
    /// </summary>
    public class ToDoLogic : IToDoLogic
    {
        private readonly PriorityTaskDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoLogic"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ToDoLogic(PriorityTaskDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all TodoItems asynchronously.
        /// </summary>
        public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific TodoItem by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to retrieve.</param>
        public async Task<TodoItem> GetTodoItemByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        /// <summary>
        /// Adds a new TodoItem asynchronously.
        /// </summary>
        /// <param name="todoItem">The TodoItem to add.</param>
        public async Task<TodoItem> AddTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        /// <summary>
        /// Updates an existing TodoItem asynchronously.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to update.</param>
        /// <param name="todoItem">The updated TodoItem.</param>
        public async Task<bool> UpdateTodoItemAsync(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return false;
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
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// Deletes a TodoItem by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to delete.</param>
        public async Task<bool> DeleteTodoItemAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
