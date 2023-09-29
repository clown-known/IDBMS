using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models;

public class IdtDbContext : DbContext
{
    public IdtDbContext() : base()
    {
    }

    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<RecoveryCode> RecoveryCodes { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<Participation> Participations { get; set; } = default!;

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
            .HasOne(project => project.ProjectOwner)
            .WithMany(projectOwner => projectOwner.OwnedProjects)
            .HasForeignKey(project => project.ProjectOwnerUserId)
            .IsRequired(false);

        modelBuilder.Entity<Project>()
            .HasOne(project => project.LeadArchitect)
            .WithMany(leadArchitect => leadArchitect.LeadProjects)
            .HasForeignKey(project => project.LeadArchitectUserId)
            .IsRequired(false);
    }
}
