using Microsoft.EntityFrameworkCore;
using MinimalAPI_Task.Models;

namespace MinimalAPI_Task.Endpoints
{
    public static class PersonModule
    {
        public static void AddPersonEndpoints(this WebApplication app)
        {
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
        }
    }
}
