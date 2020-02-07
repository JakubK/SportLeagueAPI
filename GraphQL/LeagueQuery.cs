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

            Field<ListGraphType<SettlementType>>("topSettlements",arguments: new QueryArguments(new QueryArgument<IntGraphType>
            {
                Name = "top"
            }), 
            resolve: context => 
            {
                int top = context.GetArgument<int>("top");
                return dbContext.Settlements
                .Include(x => x.Players)
                .ThenInclude(y => y.Scores)
                .OrderByDescending(x => x.Players.Sum(y => y.Scores.Sum(z => z.Points))).Take(top);
            });

            Field<SettlementType>("settlement", arguments: 
                new QueryArguments(new QueryArgument<IntGraphType>{
                Name = "id"
            }), resolve: context => 
            {
                var id = context.GetArgument<int>("id");
                return dbContext.Settlements.FirstOrDefault(x => x.Id == id);
            });

            Field<ListGraphType<EventType>>("events", resolve: context => dbContext.Events
            .Include(x => x.Scores)
            .Include(x => x.Medias));

            Field<ListGraphType<EventType>>("topEvents", 
            arguments: new QueryArguments(new QueryArgument<IntGraphType>{ Name = "top"}),
            resolve: context => {
                var top = context.GetArgument<int>("top");
                return dbContext.Events
                .Include(x => x.Scores)
                .Include(x => x.Medias)
                .OrderByDescending(x => x.Scores.Sum(y => y.Points))
                .Take(top);
            });

            Field<ListGraphType<NewsType>>("newses",resolve: context => dbContext.Newses
            .Include(x => x.Media));

            Field<ListGraphType<NewsType>>("topNews",arguments: new QueryArguments(new QueryArgument<IntGraphType>
            {
                Name = "top"
            }), resolve: context => 
            {
                var top = context.GetArgument<int>("top");
                return dbContext.Newses.OrderByDescending(x => x.Date).Take(top).Include(x => x.Media);
            });

            Field<ListGraphType<PlayerType>>("topPlayers", arguments:
            new QueryArguments(new QueryArgument<IntGraphType>
            {
                Name = "top"
            }), resolve: context => 
            {
                var top = context.GetArgument<int>("top");
                return dbContext.Players
                .Include(x => x.Settlement)
                .Include(x => x.Scores)
                .OrderByDescending(x => x.Scores.Sum(y => y.Points)).Take(top);
            });
        }
    }
}