using Microsoft.EntityFrameworkCore;
using SimpleTODO.Models;

namespace SimpleTODO
{
    public class PriorityTaskDbContext : DbContext
    {
        public PriorityTaskDbContext(DbContextOptions<PriorityTaskDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
