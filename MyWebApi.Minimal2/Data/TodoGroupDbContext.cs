using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyWebApi.Minimal2.Data
{
    public class TodoGroupDbContext : DbContext
    {
        public TodoGroupDbContext(DbContextOptions<TodoGroupDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos => Set<Todo>();


    }
}
