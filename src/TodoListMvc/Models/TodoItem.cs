

namespace TodoListMvc.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TodoItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the 'todo' item.
        /// </summary>
        [Key]
        public int Id { get; set; }


        public bool IsDone { get; set; }

        public DateTime DateModified { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
