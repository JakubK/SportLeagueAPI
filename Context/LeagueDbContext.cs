using Microsoft.EntityFrameworkCore;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.Context
{
    public class LeagueDbContext : DbContext
    {
        public LeagueDbContext(DbContextOptions<LeagueDbContext> options) : base(options) {}

        public DbSet<Player> Players {get;set;}
        public DbSet<News> Newses {get;set;}
        public DbSet<Settlement> Settlements {get;set;}
        public DbSet<Event> Events {get;set;}
    }
}