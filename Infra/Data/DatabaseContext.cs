using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Infra.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options) { }

    public DbSet<TodoItem> Todos => Set<TodoItem>();
}
