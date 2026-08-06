using JWTAuthenticationForApi.Data;
using JWTAuthenticationForApi.IService;
using JWTAuthenticationForApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


/*******************************Step 39: JWT Authentication Setting:Start*******************************/
//Step 38: Add JWT Authentication setting for API.
builder.Services.AddAuthentication
    (Options =>
        {
            Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key")))
        };
    }

    );
/*******************************Step 39: JWT Authentication Setting:End*******************************/


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// To Register  IAuthService,AuthService and Defining it scope through Dependency injection.
builder.Services.AddScoped<IAuthService, AuthService>();

// To Register  IEmployeeService,EmployeeService and Defining it scope through Dependency injection.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

/*******************************Step 40: JWT Authentication Setting:Start*******************************/
app.UseAuthentication();
/*******************************Step 40:JWT Authentication Setting:End*******************************/

app.UseAuthorization();

app.MapControllers();

app.Run();
