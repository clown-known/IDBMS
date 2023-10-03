using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models;

public class IdtDbContext : DbContext
{
    public IdtDbContext() : base()
    {
    }

    public DbSet<ConstructionTask> ConstructionTasks { get; set; } = default!;
    public DbSet<ConstructionTaskCategory> ConstructionTaskCategories { get; set; } = default!;
    public DbSet<DecorProgressReport> DecorProgressReports { get; set; } = default!;
    public DbSet<Participation> Participations { get; set; } = default!;
    public DbSet<PrepayStage> PrepayStages { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<ProjectCategory> ProjectCategories { get; set; } = default!;
    public DbSet<ProjectDocument> ProjectDocuments { get; set; } = default!;
    public DbSet<RecoveryCode> RecoveryCodes { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    
    private static string? GetConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        return config.GetConnectionString("IDBMS");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .Property(project => project.Status)
            .HasConversion<int>();

        modelBuilder.Entity<Project>()
            .Property(project => project.Type)
            .HasConversion<int>();

        modelBuilder.Entity<User>()
            .Property(user => user.Status)
            .HasConversion<int>();
    }
}
