using Data.Contexts;
using Data.Repositories;
using WebApi.Services;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi.Services.Interfaces;
using Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Lägg till DbContext och koppling till SQL Server-databas.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<StatusTypeService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<StatusTypeRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hjälp av chatGPT, den tillåter alla HTTP metoder/headers. 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Aktiverar Swagger i utvecklingsstadie.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// API start.
app.Run();

