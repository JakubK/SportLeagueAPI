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
            Field(x => x.Description);
            Field(x => x.Date);
            Field(x => x.Season);
            
            Field<ListGraphType<StringGraphType>>("medias", resolve: context => 
            {
                return context.Source.Medias.Select(x => x.Url);
            });
            Field<ListGraphType<ScoreType>>("scores",resolve: x => x.Source.Scores);
        }
    }
}