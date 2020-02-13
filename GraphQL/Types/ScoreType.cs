using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL.Types
{
    public class ScoreType : ObjectGraphType<Score>
    {
        public ScoreType()
        {
            Field(x => x.Id);
            Field(x => x.Points);
            Field(
                name: "event",
                type: typeof(EventType),
                resolve: context => context.Source.Event
            );
        }
    }
}