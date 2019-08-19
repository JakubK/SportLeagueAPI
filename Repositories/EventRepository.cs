using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportLeagueAPI.Context;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.Repositories
{
    public class EventRepository
    {
        LeagueDbContext context;
        public EventRepository(LeagueDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Event> GetAll()
        {
            return context.Events.ToList();
        }
    }
}