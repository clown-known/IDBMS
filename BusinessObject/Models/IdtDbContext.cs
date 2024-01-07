using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models;

public class IdtDbContext : DbContext
{
    public IdtDbContext() : base()
    {
    }

    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<AuthenticationCode> AuthenticationCodes { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<ProjectTask> ProjectTasks { get; set; } = default!;
    public DbSet<TaskCategory> TaskCategories { get; set; } = default!;
    public DbSet<TaskDesign> TaskDesigns { get; set; } = default!;
    public DbSet<TaskReport> TaskReports { get; set; } = default!;
    public DbSet<TaskDocument> TaskDocuments { get; set; } = default!;
    public DbSet<TaskAssignment> TaskAssignments { get; set; } = default!;
    public DbSet<Floor> Floors { get; set; } = default!;
    public DbSet<InteriorItem> InteriorItems { get; set; } = default!;
    public DbSet<InteriorItemBookmark> InteriorItemBookmarks { get; set; } = default!;
    public DbSet<InteriorItemCategory> InteriorItemCategories { get; set; } = default!;
    public DbSet<InteriorItemColor> InteriorItemColors { get; set; } = default!;
    public DbSet<Notification> Notifications { get; set; } = default!;
    public DbSet<ProjectParticipation> ProjectParticipations { get; set; } = default!;
    public DbSet<PaymentStage> PaymentStages { get; set; } = default!;
    public DbSet<PaymentStageDesign> PaymentStageDesigns { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<ProjectDesign> ProjectDesigns { get; set; } = default!;
    public DbSet<ProjectCategory> ProjectCategories { get; set; } = default!;
    public DbSet<ProjectDocument> ProjectDocuments { get; set; } = default!;
    public DbSet<ProjectDocumentTemplate> ProjectDocumentTemplates { get; set; } = default!;
    public DbSet<Room> Rooms { get; set; } = default!;
    public DbSet<RoomType> RoomTypes { get; set; } = default!;
    public DbSet<Site> Sites { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<BookingRequest> BookingRequests { get; set; } = default!;
    public DbSet<WarrantyClaim> WarrantyClaims { get; set; } = default!;
    public DbSet<ItemInTask> ItemInTasks { get; set; } = default!;

    private static string? GetConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        return config.GetConnectionString("IDBMS");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentStage>()
            .Property(stage => stage.Status)
            .HasConversion<int>();
        
        modelBuilder.Entity<Admin>()
            .Property(admin => admin.Status)
            .HasConversion<int>();

        modelBuilder.Entity<BookingRequest>()
            .Property(bookingRequest => bookingRequest.Status)
            .HasConversion<int>();

        modelBuilder.Entity<AuthenticationCode>()
            .Property(authenticationCode => authenticationCode.Status)
            .HasConversion<int>();

        modelBuilder.Entity<ProjectTask>()
            .Property(projectTask => projectTask.CalculationUnit)
            .HasConversion<int>();

        modelBuilder.Entity<TaskDesign>()
            .Property(taskDesign => taskDesign.CalculationUnit)
            .HasConversion<int>();            
        
        modelBuilder.Entity<InteriorItem>()
            .Property(interiorItem => interiorItem.CalculationUnit)
            .HasConversion<int>();          
        
        modelBuilder.Entity<InteriorItemColor>()
            .Property(interiorItemColor => interiorItemColor.Type)
            .HasConversion<int>();
        
        modelBuilder.Entity<Comment>()
            .Property(comment => comment.Status)
            .HasConversion<int>();        

        modelBuilder.Entity<ProjectDesign>()
            .Property(projectTask => projectTask.ProjectType)
            .HasConversion<int>();

        modelBuilder.Entity<TaskCategory>()
            .Property(taskCategory => taskCategory.ProjectType)
            .HasConversion<int>();

        modelBuilder.Entity<ProjectTask>()
            .Property(projectTask => projectTask.Status)
            .HasConversion<int>();  

        modelBuilder.Entity<ProjectDocumentTemplate>()
            .Property(projectDocumentTemplate => projectDocumentTemplate.Type)
            .HasConversion<int>();

        modelBuilder.Entity<InteriorItem>()
            .Property(interiorItem => interiorItem.Status)
            .HasConversion<int>();        
        
        modelBuilder.Entity<InteriorItemCategory>()
            .Property(interiorItemCategory => interiorItemCategory.InteriorItemType)
            .HasConversion<int>();        
        
        modelBuilder.Entity<User>()
            .Property(user => user.Language)
            .HasConversion<int>();

        modelBuilder.Entity<Project>()
            .Property(project => project.Language)
            .HasConversion<int>();        
        
        modelBuilder.Entity<ProjectDocumentTemplate>()
            .Property(projectDocumentTemplate => projectDocumentTemplate.Language)
            .HasConversion<int>();

        modelBuilder.Entity<Notification>()
            .Property(notification => notification.Category)
            .HasConversion<int>();

        modelBuilder.Entity<ProjectParticipation>()
            .Property(participation => participation.Role)
            .HasConversion<int>();

        modelBuilder.Entity<ProjectDocument>()
            .Property(projectDocument => projectDocument.Category)
            .HasConversion<int>();

        modelBuilder.Entity<Project>()
            .Property(project => project.Status)
            .HasConversion<int>();

        modelBuilder.Entity<Project>()
            .Property(project => project.AdvertisementStatus)
            .HasConversion<int>();

        modelBuilder.Entity<Project>()
            .Property(project => project.Type)
            .HasConversion<int>();

        modelBuilder.Entity<Transaction>()
            .Property(transaction => transaction.Status)
            .HasConversion<int>();

        modelBuilder.Entity<Transaction>()
            .Property(transaction => transaction.Type)
            .HasConversion<int>();

        modelBuilder.Entity<User>()
            .Property(user => user.Role)
            .HasConversion<int>();

        modelBuilder.Entity<User>()
            .Property(user => user.Status)
            .HasConversion<int>();


    }
}
