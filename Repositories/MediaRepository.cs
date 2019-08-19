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
        public IQueryable<Media> GetMultipleByOwnerId(int id)
        {
            return ctx.Medias.Where(x => x.OwnerId == id);
        }
    }
}