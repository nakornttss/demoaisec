using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.Data
{
#pragma warning disable
    public class GenericContext : DbContext
    {
        public GenericContext(DbContextOptions<GenericContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }

        public int RunQuery(string q)
        {
            // Only run raw SQL if provider is relational, otherwise do nothing (for InMemory)
            if (Database.ProviderName != null && Database.ProviderName.Contains("InMemory"))
            {
                // Simulate success, but do nothing
                return 0;
            }
            return Database.ExecuteSqlRaw(q);
        }
    }
#pragma warning restore
}
