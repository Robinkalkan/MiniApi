using Microsoft.EntityFrameworkCore;
using MiniApi.Classes;

namespace MiniApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Interest> Interests { get; set; }
        public DbSet<InterestLink> InterestLinks { get; set; }
        public DbSet<Person> Person { get; set; }

        public DataContext (DbContextOptions<DataContext> options) : base(options) { }

    }
}