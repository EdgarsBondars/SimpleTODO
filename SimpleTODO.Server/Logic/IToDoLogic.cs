using SimpleTODO.Models;

namespace SimpleTODO.Server.Logic
{
    /// <summary>
    /// Interface for logic related to TodoItems.
    /// </summary>
    public interface IToDoLogic
    {
        /// <summary>
        /// Retrieves all TodoItems asynchronously.
        /// </summary>
        Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync();

        /// <summary>
        /// Retrieves a specific TodoItem by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to retrieve.</param>
        Task<TodoItem> GetTodoItemByIdAsync(int id);

        /// <summary>
        /// Adds a new TodoItem asynchronously.
        /// </summary>
        /// <param name="todoItem">The TodoItem to add.</param>
        Task<TodoItem> AddTodoItemAsync(TodoItem todoItem);

        /// <summary>
        /// Updates an existing TodoItem asynchronously.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to update.</param>
        /// <param name="todoItem">The updated TodoItem.</param>
        Task<bool> UpdateTodoItemAsync(int id, TodoItem todoItem);

        /// <summary>
        /// Deletes a TodoItem by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to delete.</param>
        Task<bool> DeleteTodoItemAsync(int id);
    }
}
