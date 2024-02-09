using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tutorialAPI.Configs;
using tutorialAPI.Repositories;
using tutorialAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppTutorialContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("localConnection")!));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "");

//Validation
var tokenParams = new TokenValidationParameters()
{
    ValidateAudience = false,
    ValidateIssuer = false,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = false,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    //par d�faut C# fixe le lifetime � 5 min
    ClockSkew = TimeSpan.FromMinutes(0)
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = tokenParams;
        option.RequireHttpsMetadata = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
