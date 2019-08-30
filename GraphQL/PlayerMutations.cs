using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;
using SportLeagueAPI.Services;

namespace SportLeagueAPI.GraphQL
{
    public class PlayerMutations
    {

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Player>> AddPlayer(LeagueDbContext context, CreatePlayer args)
        {
            var newPlayer = new Player();
            newPlayer.Name = args.Name;

            newPlayer.MediaId = context.Medias.FirstOrDefault(x => x.Url == args.Link).Id;

            context.Players.Add(newPlayer);
            context.SaveChanges();

            return ctx => context.Players.First(x => x.Id == newPlayer.Id);
        }
    }
}