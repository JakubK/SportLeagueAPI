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
        public DbSet<Media> Medias {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(new Event{ Id= 1, Name = "Cool Event"});
        }
    }
}