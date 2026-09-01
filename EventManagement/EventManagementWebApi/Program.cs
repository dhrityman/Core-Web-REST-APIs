using EventManagementWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Step 2.1.4:Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{ 
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
