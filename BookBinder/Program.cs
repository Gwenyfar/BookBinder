using Autofac;
using BookBinder.Application.Services;
using BookBinder.Application.Services.AuthorFeatures;
using BookBinder.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

var appConfig = configuration.ExtractAppSettings(LoggerFactory.Create(l => l.AddConsole()));

var bootstrapper = new Bootstrapper(appConfig);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(AuthorService));
builder.Services.AddScoped(c=>bootstrapper.Container);

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
