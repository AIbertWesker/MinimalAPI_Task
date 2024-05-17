using Microsoft.EntityFrameworkCore;
using MinimalAPI_Task;
using MinimalAPI_Task.Models;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FakeDb>(opt => opt.UseInMemoryDatabase("PersonList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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


app.MapGet("/personitems", async (FakeDb db) =>
    await db.People.ToListAsync());

app.MapGet("/personitems/{id}", async (int id, FakeDb db) =>
    await db.People.FindAsync(id)
        is Person person
            ? Results.Ok(person)
            : Results.NotFound());

app.MapPost("/personitems", async (Person person, FakeDb db) =>
{
    db.People.Add(person);
    await db.SaveChangesAsync();

    return Results.Created($"/personitems/{person.Id}", person);
});

app.Run();
