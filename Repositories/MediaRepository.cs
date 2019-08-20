using System.Collections.Generic;
using System.Linq;
using SportLeagueAPI.Context;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.Repositories
{
    public class MediaRepository
    {
        LeagueDbContext ctx;
        public MediaRepository(LeagueDbContext context)
        {
            ctx = context;
        }
    }
}