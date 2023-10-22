using API.Supporters;
using API.Supporters.JwtAuthSupport;
using BLL.Services;
using Repository.Implements;
using Repository.Interfaces;

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

builder.Services.AddScoped<FirebaseService, FirebaseService>();
builder.Services.AddScoped<JwtTokenSupporter, JwtTokenSupporter>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JWTMiddleware>();

app.MapControllers();

app.Run();
