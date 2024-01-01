using API.Supporters;
using API.Supporters.JwtAuthSupport;
using BLL.Services;
<<<<<<< HEAD
using Repository.Implements;
using Repository.Interfaces;
=======
using BusinessObject.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository.Implements;
using Repository.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json.Serialization;
>>>>>>> dev

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD

=======
string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "idbms-7f5e1-firebase-adminsdk-er69h-99ecd4346c.json");
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(jsonFilePath),
    ProjectId = builder.Configuration["Firebase:ProjectId"]
});
>>>>>>> dev
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

<<<<<<< HEAD
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IParticipationRepository, ParticipationRepository>();

=======
//add jwt bearer to swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IDBMS API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
});

// repository
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAuthenticationCodeRepository, AuthenticationCodeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITaskCategoryRepository, TaskCategoryRepository>();
builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>(); 
builder.Services.AddScoped<ITaskDesignRepository, TaskDesignRepository>();
builder.Services.AddScoped<ITaskReportRepository, TaskReportRepository>();
builder.Services.AddScoped<ITaskDocumentRepository, TaskDocumentRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IInteriorItemBookmarkRepository, InteriorItemBookmarkRepository>();
builder.Services.AddScoped<IInteriorItemCategoryRepository, InteriorItemCategoryRepository>();
builder.Services.AddScoped<IInteriorItemColorRepository, InteriorItemColorRepository>();
builder.Services.AddScoped<IInteriorItemRepository, InteriorItemRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IProjectParticipationRepository, ProjectParticipationRepository>();
builder.Services.AddScoped<IPaymentStageRepository, PaymentStageRepository>();
builder.Services.AddScoped<IPaymentStageDesignRepository, PaymentStageDesignRepository>();
builder.Services.AddScoped<IProjectCategoryRepository, ProjectCategoryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectDesignRepository, ProjectDesignRepository>();
builder.Services.AddScoped<IProjectDocumentRepository, ProjectDocumentRepository>();
builder.Services.AddScoped<IProjectDocumentTemplateRepository, DocumentTemplateRepository>();
builder.Services.AddScoped<ITaskAssignmentRepository, TaskAssignmentRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRequestRepository, BookingRequestRepository>();
builder.Services.AddScoped<IWarrantyClaimRepository, WarrantyClaimRepository>();
builder.Services.AddScoped<IItemInTaskRepository, ItemInTaskRepository>();

// service
builder.Services.AddScoped<AdminService, AdminService>();
builder.Services.AddScoped<AuthenticationCodeService, AuthenticationCodeService>();
builder.Services.AddScoped<CommentService, CommentService>();
builder.Services.AddScoped<ContractService, ContractService>();
builder.Services.AddScoped<TaskCategoryService, TaskCategoryService>();
builder.Services.AddScoped<ProjectTaskService, ProjectTaskService>();
builder.Services.AddScoped<TaskDesignService, TaskDesignService>();
builder.Services.AddScoped<TaskReportService, TaskReportService>();
builder.Services.AddScoped<TaskDocumentService, TaskDocumentService>();
builder.Services.AddScoped<FloorService, FloorService>();
builder.Services.AddScoped<InteriorItemBookmarkService, InteriorItemBookmarkService>();
builder.Services.AddScoped<InteriorItemCategoryService, InteriorItemCategoryService>();
builder.Services.AddScoped<InteriorItemColorService, InteriorItemColorService>();
builder.Services.AddScoped<InteriorItemService, InteriorItemService>();
builder.Services.AddScoped<NotificationService, NotificationService>();
builder.Services.AddScoped<ProjectParticipationService, ProjectParticipationService>();
builder.Services.AddScoped<PaymentStageService, PaymentStageService>();
builder.Services.AddScoped<PaymentStageDesignService, PaymentStageDesignService>();
builder.Services.AddScoped<ProjectCategoryService, ProjectCategoryService>();
builder.Services.AddScoped<ProjectService, ProjectService>();
builder.Services.AddScoped<ProjectDesignService, ProjectDesignService>();
builder.Services.AddScoped<ProjectDocumentService, ProjectDocumentService>();
builder.Services.AddScoped<TaskAssignmentService, TaskAssignmentService>();
builder.Services.AddScoped<DocumentTemplateService, DocumentTemplateService>();
builder.Services.AddScoped<RoomService, RoomService>();
builder.Services.AddScoped<RoomTypeService, RoomTypeService>();
builder.Services.AddScoped<SiteService, SiteService>();
builder.Services.AddScoped<TransactionService, TransactionService>();
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<BookingRequestService, BookingRequestService>();
builder.Services.AddScoped<WarrantyClaimService, WarrantyClaimService>();
builder.Services.AddScoped<ItemInTaskService, ItemInTaskService>();

builder.Services.AddScoped<AdvertisementService, AdvertisementService>();
builder.Services.AddScoped<DashboardService, DashboardService>();

builder.Services.AddScoped(typeof(PaginationService<>), typeof(PaginationService<>));
>>>>>>> dev

builder.Services.AddScoped<FirebaseService, FirebaseService>();
builder.Services.AddScoped<JwtTokenSupporter, JwtTokenSupporter>();


// cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:3001"
                                                //, "http://localhost:7979"
                                                )
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IDBMS API V1");
    });
}

app.UseHttpsRedirection();

// cors
app.UseCors();

app.UseAuthorization();

app.UseMiddleware<JWTMiddleware>();

app.MapControllers();

app.Run();
<<<<<<< HEAD
=======


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

    builder.EntitySet<Admin>("Admins");
    builder.EntitySet<AuthenticationCode>("AuthenticationCodes");
    builder.EntitySet<Comment>("Comments"); 
    builder.EntitySet<Floor>("Floors");
    builder.EntitySet<InteriorItem>("InteriorItems");
    builder.EntitySet<InteriorItemBookmark>("InteriorItemBookmarks");
    builder.EntitySet<InteriorItemCategory>("InteriorItemCategories");
    builder.EntitySet<InteriorItemColor>("InteriorItemColors");
    builder.EntitySet<Notification>("Notifications");
    builder.EntitySet<ProjectParticipation>("ProjectParticipations");
    builder.EntitySet<PaymentStage>("PaymentStages");
    builder.EntitySet<PaymentStageDesign>("PaymentStageDesigns");
    builder.EntitySet<Project>("Projects");
    builder.EntitySet<ProjectDesign>("ProjectDesigns");
    builder.EntitySet<ProjectCategory>("ProjectCategories");
    builder.EntitySet<ProjectDocument>("ProjectDocuments");
    builder.EntitySet<ProjectDocumentTemplate>("DocumentTemplates");
    builder.EntitySet<ProjectTask>("ProjectTasks");
    builder.EntitySet<Room>("Rooms");
    builder.EntitySet<RoomType>("RoomTypes");
    builder.EntitySet<Site>("Sites");
    builder.EntitySet<TaskAssignment>("TaskAssignments");
    builder.EntitySet<TaskCategory>("TaskCategories");
    builder.EntitySet<TaskDesign>("TaskDesigns");
    builder.EntitySet<TaskDocument>("TaskDocuments");
    builder.EntitySet<TaskReport>("TaskReports");
    builder.EntitySet<Transaction>("Transactions");
    builder.EntitySet<User>("Users");
    builder.EntitySet<BookingRequest>("BookingRequests");
    builder.EntitySet<WarrantyClaim>("WarrantyClaims");
    builder.EntitySet<ItemInTask>("ItemInTasks");

    return builder.GetEdmModel();
}
>>>>>>> dev
