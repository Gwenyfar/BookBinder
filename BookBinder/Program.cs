using BookBinder.Application;
using BookBinder.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var appConfig = configuration.ExtractAppSettings(LoggerFactory.Create(l => l.AddConsole()));
var bootstrapper = new Bootstrapper(appConfig);

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.Authority = "https://localhost:5001";
    //o.Audience = "bookbinderapi";
    o.TokenValidationParameters.ValidateAudience = false;
    o.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
});
builder.Services.AddControllers(o=> o.Filters.Add(new AuthorizeFilter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"bookbinderapi", "Access to Bookbinder API" },
                }
            }
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
    var path = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var pathString = Path.Combine(AppContext.BaseDirectory, path);
    options.IncludeXmlComments(pathString);
});
builder.Services.AddScoped<IApplication>(a => bootstrapper.Application);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
