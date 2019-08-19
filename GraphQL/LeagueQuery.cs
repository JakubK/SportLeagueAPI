using System.Linq;
using GraphQL.Types;
using SportLeagueAPI.Context;
using SportLeagueAPI.GraphQL.Types;
using SportLeagueAPI.Repositories;

namespace SportLeagueAPI.GraphQL
{
    public class LeagueQuery : ObjectGraphType
    {
        public LeagueQuery(EventRepository eventRepository)
        {
            Field<ListGraphType<EventType>>("events", resolve: context => eventRepository.GetAll());
        }
    }
}