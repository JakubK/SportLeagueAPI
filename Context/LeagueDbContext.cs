using Microsoft.EntityFrameworkCore;
using SportLeagueAPI.DTO;

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
        public DbSet<Score> Scores {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Medias
            modelBuilder.Entity<Media>().HasData(
                new Media{ Id=1, Url="https://google.com" },
                new Media{ Id=2, Url="https://wp.pl" },

                new Media{ Id=3, Url="https://wp.pl" },
                new Media{ Id=4, Url="https://wp.pl" },
                new Media{ Id=5, Url="https://wp.pl" },

                new Media{ Id=6, Url="https://wp.pl", EventId=1 },
                new Media{ Id=7, Url="https://wp.pl", EventId=1 }
            );

            //Settlement
            modelBuilder.Entity<Settlement>().HasData(new Settlement{ Id= 1, Name = "Settlement 1", MediaId = 1});
            modelBuilder.Entity<Settlement>().HasData(new Settlement{ Id= 2, Name = "Settlement 2", MediaId = 2});

            //Events
            modelBuilder.Entity<Event>().HasData(new Event{ Id= 1, Name = "Test Event", SettlementId = 1});

            //Player
            modelBuilder.Entity<Player>().HasData(new Player{Id = 1, Name="Player 1", SettlementId = 1, MediaId=3});
            modelBuilder.Entity<Player>().HasData(new Player{Id = 2, Name="Player 2", SettlementId = 2, MediaId=4});

            //News
            modelBuilder.Entity<News>().HasData(new News{Id = 1, Description = "Sample description of news", Name="Test News", MediaId=5});

            //Score
            modelBuilder.Entity<Score>().HasData(new Score{Id = 1, Value=10, PlayerId = 1, EventId = 1});
            modelBuilder.Entity<Score>().HasData(new Score{Id = 2, Value=10, PlayerId = 2, EventId = 1});
        }
    }
}