using System.Linq;
using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL.Types
{
    public class PlayerType : ObjectGraphType<Player>
    {
        public PlayerType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(
                name: "settlement",
                type: typeof(StringGraphType),
                resolve: context => context.Source.Settlement.Name
            );
            Field(
                name: "points",
                type: typeof(IntGraphType),
                resolve: context => context.Source.Scores.Sum(y => y.Points)
            );
        }
    }
}