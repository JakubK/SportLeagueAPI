using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SportLeagueAPI.Context
{
  public class DesignTimeLeagueDbContextFactory : IDesignTimeDbContextFactory<LeagueDbContext>
  {
    public LeagueDbContext CreateDbContext(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SportLeagueAPI"))
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<LeagueDbContext>();
        builder.UseSqlite(config.GetConnectionString("Database"));
        return new LeagueDbContext(builder.Options);
    }
  }
}