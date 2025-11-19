using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Insurance.Quote.Infrastructure
{
    public class QuoteDbContextFactory : IDesignTimeDbContextFactory<QuoteDbContext>
    {
        public QuoteDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Insurance.Quote.Api");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) 
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("QuoteConnection");
            var builder = new DbContextOptionsBuilder<QuoteDbContext>();
            builder.UseNpgsql(connectionString, b =>
                b.MigrationsAssembly(typeof(QuoteDbContext).Assembly.FullName));

            return new QuoteDbContext(builder.Options);
        }
    }
}