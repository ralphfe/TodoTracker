

namespace TodoListMvc.Models
{
    using Microsoft.EntityFrameworkCore;

    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem>? TodoItems { get; set; }
    }
}
