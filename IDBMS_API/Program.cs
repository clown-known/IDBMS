using API.Supporters;
using API.Supporters.JwtAuthSupport;
using BLL.Services;
using BusinessObject.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Repository.Implements;
using Repository.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IParticipationRepository, ParticipationRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IProjectCategoryRepository, ProjectCategoryRepository>();
builder.Services.AddScoped<IConstructionTaskCategoryRepository, ConstructionTaskCategoryRepository>();
builder.Services.AddScoped<IProjectDocumentRepository, ProjectDocumentRepository>();
builder.Services.AddScoped<IProjectDocumentTemplateRepository, DocumentTemplateRepository>();
builder.Services.AddScoped<IDecorProjectDesignRepository, DecorProjectDesignRepository>();
builder.Services.AddScoped<IPrepayStageDesignRepository, PrepayStageDesignRepository>();
builder.Services.AddScoped<IAuthenticationCodeRepository, AuthenticationCodeRepository>();
builder.Services.AddScoped<FirebaseService, FirebaseService>();
builder.Services.AddScoped<JwtTokenSupporter, JwtTokenSupporter>();
builder.Services.AddControllers().AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                .AddOData(option => option.Select().Filter()
                .Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
builder.Services.AddODataQueryFilter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseODataBatching();

app.UseMiddleware<JWTMiddleware>();

app.MapControllers();

app.Run();


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

    builder.EntitySet<Admin>("Admins");
    builder.EntitySet<ApplianceSuggestion>("ApplianceSuggestions");
    builder.EntitySet<AuthenticationCode>("AuthenticationCodes");
    builder.EntitySet<Comment>("Comments"); 
    builder.EntitySet<ConstructionTask>("ConstructionTasks");
    builder.EntitySet<ConstructionTaskCategory>("ConstructionTaskCategorys"); 
    builder.EntitySet<ConstructionTaskDesign>("ConstructionTaskDesigns");
    builder.EntitySet<ConstructionTaskReport>("ConstructionTaskReports"); 
    builder.EntitySet<DecorProgressReport>("DecorProgressReports");
    builder.EntitySet<DecorProjectDesign>("DecorProjectDesigns"); 
    builder.EntitySet<Floor>("Floors");
    builder.EntitySet<InteriorItem>("InteriorItems");
    builder.EntitySet<InteriorItemBookmark>("InteriorItemBookmarks");
    builder.EntitySet<InteriorItemCategory>("InteriorItemCategorys");
    builder.EntitySet<InteriorItemColor>("InteriorItemColors");
    builder.EntitySet<Notification>("Notifications");
    builder.EntitySet<Participation>("Participations");
    builder.EntitySet<PrepayStage>("PrepayStages");
    builder.EntitySet<PrepayStageDesign>("PrepayStageDesigns");
    builder.EntitySet<Project>("Projects");
    builder.EntitySet<ProjectCategory>("ProjectCategorys");
    builder.EntitySet<ProjectDocument>("ProjectDocuments");
    builder.EntitySet<ProjectDocumentTemplate>("ProjectDocumentTemplates");
    builder.EntitySet<Room>("Rooms");
    builder.EntitySet<RoomType>("RoomTypes");
    builder.EntitySet<Transaction>("Transactions");
    builder.EntitySet<User>("Users");
    builder.EntitySet<UserRole>("UserRoles");

    return builder.GetEdmModel();
}