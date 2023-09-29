using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models;

public class IdtDbContext : DbContext
{
    public IdtDbContext() : base()
    {
    }

    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<Participation> Participations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.UserRoles)
            .WithOne(userRole => userRole.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(user => user.OwnedProjects)
            .WithOne(ownedProject => ownedProject.ProjectOwner)
            .HasForeignKey(ownedProject => ownedProject.ProjectOwnerUserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(user => user.LeadProjects)
            .WithOne(leadProject => leadProject.LeadArchitect)
            .HasForeignKey(leadProject => leadProject.LeadArchitectUserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(user => user.ParticipateProjects)
            .WithMany(project => project.ParticipatingUsers)
            .UsingEntity<Participation>();
    }
}
