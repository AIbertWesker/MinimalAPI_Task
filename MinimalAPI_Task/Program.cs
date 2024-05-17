using Microsoft.EntityFrameworkCore;
using MinimalAPI_Task;
using MinimalAPI_Task.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FakeDb>(opt => opt.UseInMemoryDatabase("PersonList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

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
