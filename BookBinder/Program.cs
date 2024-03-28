using BookBinder.Application;
using BookBinder.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var appConfig = configuration.ExtractAppSettings(LoggerFactory.Create(l => l.AddConsole()));
var bootstrapper = new Bootstrapper(appConfig);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Security",
        In = ParameterLocation.Header,
        Description = "Security Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

    var path = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var pathString = Path.Combine(AppContext.BaseDirectory, path);
    options.IncludeXmlComments(pathString);
});
builder.Services.AddScoped<IApplication>(a => bootstrapper.Application);
builder.Services.AddScoped<ILogger>(l => LoggerFactory.Create(l => l.AddConsole()).CreateLogger("Token"));

builder.Services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("SSO", options =>
{
    //builder.Configuration.Bind("AzureAd", options);
    options.Audience = "7d219245-4395-4cb1-b5bd-7930da7c4f0e";
    options.TokenValidationParameters.ValidIssuer = "https://login.microsoftonline.com/fa23820c-5ae0-43de-968f-69e872bc6200/v2.0";
    options.MetadataAddress = "https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration";
    options.SaveToken = true;
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.ValidateAudience = true;
    options.TokenValidationParameters.ValidateIssuer = true;
    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
    options.TokenValidationParameters.ValidateLifetime = true;
}).AddJwtBearer(options =>
{
    options.Audience = "bookbinder.com";
    options.SaveToken = true;
    options.TokenValidationParameters.ValidIssuer = "bookbinderapi";
    options.TokenValidationParameters.ValidateAudience = true;
    options.TokenValidationParameters.ValidateIssuer = true;
    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
    options.TokenValidationParameters.ValidateLifetime = true;
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bookbinder.com72984034875-234873647859"));
});

//.AddMicrosoftIdentityWebApi(options =>
//{
//    builder.Configuration.Bind("AzureAd", options);
//    //options.Audience = "7d219245-4395-4cb1-b5bd-7930da7c4f0e";
//    //options.Authority = "https://login.microsoftonline.com/fa23820c-5ae0-43de-968f-69e872bc6200/v2.0";
//    //options.MetadataAddress = "https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration";
//    options.TokenValidationParameters.NameClaimType = "name";
//    options.SaveToken = true;
//    options.TokenValidationParameters.ValidIssuer = "https://login.microsoftonline.com/fa23820c-5ae0-43de-968f-69e872bc6200/v2.0";
//    options.TokenValidationParameters.ValidateAudience = true;
//    options.TokenValidationParameters.ValidateIssuer = true;
//}, op => { builder.Configuration.Bind("AzureAd", op); });

builder.Services.AddAuthorization(c => c.AddPolicy("SSO", new AuthorizationPolicyBuilder().AddAuthenticationSchemes("SSO").RequireAuthenticatedUser().Build()));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
