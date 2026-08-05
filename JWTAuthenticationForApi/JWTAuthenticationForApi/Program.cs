using Microsoft.EntityFrameworkCore;
using JWTAuthenticationForApi.Data;
using JWTAuthenticationForApi.Services;
using JWTAuthenticationForApi.IService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// To Register  IAuthService,AuthService and Defining it scope through Dependency injection.
builder.Services.AddScoped<IAuthService,AuthService>();

// To Register  IEmployeeService,EmployeeService and Defining it scope through Dependency injection.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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
