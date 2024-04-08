using System.ComponentModel.DataAnnotations;

namespace SimpleTODO.Models
{
    /// <summary>
    /// Represents a TODO item.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Gets or sets the ID of the TODO item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the TODO item.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the TODO item.
        /// </summary>
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the TODO item is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
