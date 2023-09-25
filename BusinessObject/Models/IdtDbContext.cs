using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models;

public class IdtDbContext : DbContext
{
    public IdtDbContext() : base()
    {
    }

    public DbSet<Project> Projects { get; set; } = default!;
}
