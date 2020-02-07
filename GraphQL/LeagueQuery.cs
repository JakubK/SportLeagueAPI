using System.Linq;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.GraphQL.Types;

namespace SportLeagueAPI.GraphQL
{
    public class LeagueQuery : ObjectGraphType
    {
        public LeagueQuery(LeagueDbContext dbContext)
        {
            Field<ListGraphType<PlayerType>>("players", resolve: context => dbContext.Players
            .Include(x => x.Settlement)
            .Include(x => x.Scores));

            
            Field<ListGraphType<SettlementType>>("settlements", resolve: context => dbContext.Settlements
            .Include(x => x.Players).ThenInclude(y => y.Scores));

            Field<SettlementType>("settlement", arguments: 
                new QueryArguments(new QueryArgument<IntGraphType>{
                Name = "id"
            }), resolve: context => 
            {
                var id = context.GetArgument<int>("id");
                return dbContext.Settlements.FirstOrDefault(x => x.Id == id);
            });

            Field<ListGraphType<EventType>>("events", resolve: context => dbContext.Events
            .Include(x => x.Scores));

            Field<ListGraphType<NewsType>>("newses",resolve: context => dbContext.Newses
            .Include(x => x.Media));
        }
    }
}