using Microsoft.EntityFrameworkCore;
using StockManagement.Models;

namespace StockManagement.Data
{
    /// <summary>
    /// Database context for the StockManagement application.
    /// </summary>
    public class GenericContext : DbContext
    {
        public GenericContext(DbContextOptions<GenericContext> options) : base(options) { }

        /// <summary>
        /// Products table.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Executes a raw SQL query if using a relational provider.
        /// </summary>
        /// <param name="sql">The SQL command to execute.</param>
        /// <returns>Number of affected rows, or 0 for InMemory.</returns>
        public int RunQuery(string sql)
        {
            // Only run raw SQL if provider is relational, otherwise do nothing (for InMemory)
            if (Database.ProviderName != null && Database.ProviderName.Contains("InMemory"))
            {
                // Simulate success, but do nothing
                return 0;
            }
            return Database.ExecuteSqlRaw(sql);
        }
    }
}
