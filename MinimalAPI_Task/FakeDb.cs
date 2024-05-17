using Microsoft.EntityFrameworkCore;
using MinimalAPI_Task.Models;

namespace MinimalAPI_Task
{
    public class FakeDb : DbContext
    {
        public FakeDb(DbContextOptions<FakeDb> options)
            : base(options) { }

        public DbSet<Person> People => Set<Person>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().OwnsOne(p => p.Address);
        }
    }
}
