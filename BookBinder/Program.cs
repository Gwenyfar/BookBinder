using BookBinder.Application;
using BookBinder.Services;
using Microsoft.AspNetCore;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, 8080, listenOptions =>
    {
        listenOptions.UseHttps("./certificate.pfx", "./private.key");
    });
});
var configuration = builder.Configuration;
var appConfig = configuration.ExtractAppSettings(LoggerFactory.Create(l => l.AddConsole()));
var bootstrapper = new Bootstrapper(appConfig);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var path = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var pathString = Path.Combine(AppContext.BaseDirectory, path);
    options.IncludeXmlComments(pathString);
});
builder.Services.AddScoped<IApplication>(a => bootstrapper.Application);

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
