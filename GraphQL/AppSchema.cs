using System.Linq;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;

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

            //order entities
            schema.AddField("playersByScore",ctx => ctx.Players.OrderByDescending(x => schema.Type<Player>().GetField("points")),"Players by score descending");


            //points fields
            schema.Type<Settlement>().AddField("points", ctx => ctx.Players.Sum(x => x.Scores.Sum(y => y.Value)),"Total points of Settlement");
            schema.Type<Player>().AddField("points",ctx => ctx.Scores.Sum(y => y.Value),"Player points");

            //media fields replacement
            schema.Type<Settlement>().ReplaceField("media",ctx => ctx.Media.Url,"Url of Settlement Image");
            schema.Type<News>().ReplaceField("media", ctx => ctx.Media.Url,"Url of photo attached to News");
            schema.Type<Player>().ReplaceField("media",ctx => ctx.Media.Url,"Url of Player Photo");
            schema.Type<Event>().ReplaceField("medias", ctx => ctx.Medias.Select(x => x.Url),"Set of Urls to images for this Event");

            //Player settlement field replacement
            schema.Type<Player>().ReplaceField("settlement", ctx => ctx.Settlement.Name,"Name of Player's Settlement");

            //top fields
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
            }, (ctx,param) => ctx.Players.OrderByDescending(x => schema.Type<Player>().GetField("points")).Take(param.count),"Players","List of top Players");

            schema.AddField("topSettlements", new {
                count = 5
            }, (ctx,param) => ctx.Settlements.OrderByDescending(x => schema.Type<Settlement>().GetField("points")).Take(param.count),"Settlements","List of top Settlements");

            return schema;
        }
    }
}