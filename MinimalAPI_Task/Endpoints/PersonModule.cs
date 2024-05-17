using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_Task.Endpoints
{
    public static class PersonModule
    {
        public static void AddPersonEndpoints(this WebApplication app)
        {
            app.MapGet("/personitems", async (FakeDb db) =>
                await db.People.ToListAsync());

            app.MapGet("/personitems/{id:int}", async (int id, FakeDb db) =>
                await db.People.FindAsync(id)
                    is Person person
                        ? Results.Ok(person)
                        : Results.NotFound());

            app.MapPost("/personitems", async (IValidator<Person> _validator, Person person, FakeDb db) =>
            {
                var validationResult = await _validator.ValidateAsync(person);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.FirstOrDefault().ToString());

                db.People.Add(person);
                await db.SaveChangesAsync();

                return Results.Created($"/personitems/{person.Id}", person);
            });
        }
    }
}
