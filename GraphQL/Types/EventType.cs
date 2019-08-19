using GraphQL.Types;
using SportLeagueAPI.Context;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class EventType : ObjectGraphType<Event>
    {
        public EventType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Date);
        }
    }
}