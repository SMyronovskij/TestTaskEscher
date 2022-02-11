using Microsoft.EntityFrameworkCore;
using TestTaskEscher.DataModels.DbModels.PersonModels;

namespace TestTaskEscher.DataModel
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Person> People => Set<Person>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=testApp.db");
        }
    }
}