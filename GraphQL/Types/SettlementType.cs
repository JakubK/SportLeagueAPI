using System.Linq;
using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL.Types
{
    public class SettlementType : ObjectGraphType<Settlement>
    {
        public SettlementType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);

            Field(
                name: "players",
                type: typeof(ListGraphType<PlayerType>),
                resolve: context => context.Source.Players
            );
            
            Field(
                name: "points",
                type: typeof(IntGraphType),
                resolve: context => context.Source.Players
                .Sum(x => x.Scores.Sum(y => y.Points)));

            Field(
                name: "media",
                type: typeof(StringGraphType),
                resolve: context => context.Source.Media.Url
            );
        }
    }
}