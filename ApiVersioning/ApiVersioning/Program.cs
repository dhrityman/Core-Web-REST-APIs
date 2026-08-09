using Microsoft.EntityFrameworkCore;
using ApiVersioning.Data;
using ApiVersioning.Services;
using ApiVersioning.IService;
using Asp.Versioning.Conventions;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();

/************Step 40:API Versioning Setting :Start**********************************/
builder.Services.AddApiVersioning(options =>
{
    //AssumeDefaultVersionWhenUnspecified:=> Application will execute with default version.
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    // This will ensure response will support the corresding version.
    options.ReportApiVersions = true;
    //Step 44:
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("api-version")
        );

}).AddMvc(options =>
{
    options.Conventions.Add(new VersionByNamespaceConvention());

}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

/************Step 40:Add API Versioning Setting :End**********************************/

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
