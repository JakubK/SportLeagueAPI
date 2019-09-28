using System;
using System.Linq;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL
{
    public class AppSchema
    {
        public static MappedSchemaProvider<LeagueDbContext> MakeSchema()
        {
            var schema = SchemaBuilder.FromObject<LeagueDbContext>();

            schema.AddType(typeof(JsonWebToken),"jsonWebToken","Token");

            schema.AddMutationFrom(new AuthMutations());

            schema.AddMutationFrom(new EventMutations());
            schema.AddMutationFrom(new NewsMutations());
            schema.AddMutationFrom(new SettlementMutations());
            schema.AddMutationFrom(new PlayerMutations());

            //search queries
            schema.AddField("findPlayers", new{
                phrase = ""
            }, (ctx, param) => ctx.Players.Where( x=> x.Name.ToLower().Contains(param.phrase)),"Query that returns set of Players that contain passed phrase in their names");
            
            schema.AddField("findEvents", new{
                phrase = ""
            }, (ctx, param) => ctx.Events.Where( x=> x.Name.ToLower().Contains(param.phrase)),"Query that returns set of Events that contain passed phrase in their names");

            schema.AddField("findNewses", new{
                phrase = ""
            }, (ctx, param) => ctx.Newses.Where( x=> x.Name.ToLower().Contains(param.phrase)),"Query that returns set of Newses that contain passed phrase in their names");

            schema.AddField("findSettlements", new{
                phrase = ""
            }, (ctx, param) => ctx.Settlements.Where( x=> x.Name.ToLower().Contains(param.phrase)),"Query that returns set of Settlements that contains passed phrase in their names");

            schema.Type<Score>().AddField("playerName", ctx => ctx.Player.Name,"Name of Score owner");

            schema.Type<Event>().AddField("players", ctx => ctx.Scores.Select(x => x.Player),"Players");

            //points fields     
            schema.Type<Settlement>().AddField("points", ctx => ctx.Players.Sum(x => x.Scores.Sum(y => y.Points)),"Total points of Settlement");
            schema.Type<Player>().AddField("points",ctx => ctx.Scores.Sum(y => y.Points),"Player points");
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