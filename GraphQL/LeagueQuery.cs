using System.Linq;
using GraphQL.Types;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL
{
    public class LeagueQuery : ObjectGraphType
    {
        public LeagueQuery(LeagueDbContext dbContext)
        {
            Field<ListGraphType<PlayerType>>("players", resolve: context => dbContext.Players.AsQueryable());
        }
    }
}