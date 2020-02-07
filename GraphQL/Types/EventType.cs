using System.Linq;
using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL.Types
{
    public class EventType : ObjectGraphType<Event>
    {
        public EventType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Date);
            Field<ListGraphType<ScoreType>>("scores",resolve: x => x.Source.Scores);
        }
    }
}