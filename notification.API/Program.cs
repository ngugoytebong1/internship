using Microsoft.EntityFrameworkCore;
using notification.API.Data; // Ensure this matches your project's namespace
using notification.API.Services;
var builder = WebApplication.CreateBuilder(args);


// --- START: NEW DATABASE CONFIGURATION ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// --- END: NEW DATABASE CONFIGURATION ---

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
// The rest of your existing code continues from 
// ...
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
// Add services to the container.
builder.Services.AddScoped<INotificationService, NotificationService>();
//Add services
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
