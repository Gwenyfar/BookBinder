using BookBinder.Application;
using BookBinder.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var appConfig = configuration.ExtractAppSettings(LoggerFactory.Create(l => l.AddConsole()));
var bootstrapper = new Bootstrapper(appConfig);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddControllers(o=> o.Filters.Add(new AuthorizeFilter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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
