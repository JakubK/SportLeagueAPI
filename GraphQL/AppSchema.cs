using System.Linq;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;

namespace SportLeagueAPI.GraphQL
{
    public class AppSchema
    {
        public static MappedSchemaProvider<LeagueDbContext> MakeSchema()
        {
            var schema = SchemaBuilder.FromObject<LeagueDbContext>();

            schema.AddMutationFrom(new EventMutations());
            schema.AddMutationFrom(new NewsMutations());
            schema.AddMutationFrom(new SettlementMutations());
            schema.AddMutationFrom(new PlayerMutations());

            schema.AddField("topNews",new {
                count = 5
            }, (ctx, param) => ctx.Newses.OrderByDescending(x => x.Date).Take(param.count)
            ,"Newses","List of Newses");

            schema.AddField("topEvents",new {
                count = 5
            }, (ctx, param) => ctx.Events.OrderByDescending(x => x.Date).Take(param.count)
            ,"Events","List of Events");

            schema.AddField("topPlayers", new {
                count = 5
            }, (ctx,param) => ctx.Players.OrderByDescending(x => x.Scores.Sum(y => y.Value)).Take(param.count),"Players","List of top Players");

            schema.AddField("topSettlements", new {
                count = 5
            }, (ctx,param) => ctx.Settlements.OrderByDescending(x => x.Players.Sum(y => y.Scores.Sum(z => z.Value))).Take(param.count),"Settlements","List of top Settlements");

            return schema;
        }
    }
}