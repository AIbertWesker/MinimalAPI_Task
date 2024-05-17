using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Task;
using MinimalAPI_Task.Converters;
using MinimalAPI_Task.Endpoints;
using MinimalAPI_Task.Models;
using MinimalAPI_Task.Validations;
using NSwag.AspNetCore;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FakeDb>(opt => opt.UseInMemoryDatabase("PersonList"));

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new DateOnlyJsonConverter());
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddValidatorsFromAssemblyContaining<PersonValidation>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "PersonAPI";
    config.Title = "PersonAPI v1";
    config.Version = "v1";
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "PersonAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.AddPersonEndpoints();

app.Run();
