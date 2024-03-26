using BookBinder.Application;
using BookBinder.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
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

builder.Services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                {
                    //builder.Configuration.Bind("AzureAd", options);
                    options.Audience = "7d219245-4395-4cb1-b5bd-7930da7c4f0e";
                    options.Authority = "https://login.microsoftonline.com/fa23820c-5ae0-43de-968f-69e872bc6200/v2.0";
                    options.MetadataAddress = "https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration";
                    options.SaveToken = true;
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.ValidateAudience = true;
                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                    options.TokenValidationParameters.ValidateLifetime = true;
                });

//.AddMicrosoftIdentityWebApi(options =>
//{
//    builder.Configuration.Bind("AzureAd", options);
//    //options.Audience = "7d219245-4395-4cb1-b5bd-7930da7c4f0e";
//    //options.Authority = "https://login.microsoftonline.com/fa23820c-5ae0-43de-968f-69e872bc6200/v2.0";
//    //options.MetadataAddress = "https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration";
//    options.TokenValidationParameters.NameClaimType = "name";
//    options.SaveToken = true;
//    options.TokenValidationParameters.ValidateAudience = true;
//    options.TokenValidationParameters.ValidateIssuer = true;
//}, op => { builder.Configuration.Bind("AzureAd", op); });

//builder.Services.AddAuthorization(c => c.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).AddRequirements(new ScopeAuthorizationRequirement { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }).Build());
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
