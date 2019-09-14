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

            newPlayer.SettlementId = args.SettlementId;

            if(args.Links.Count > 0)
            {
                newPlayer.MediaId = context.Medias.FirstOrDefault(x => x.Url == args.Links[0]).Id;
            }
            context.Players.Add(newPlayer);
            context.SaveChanges();

            return ctx => context.Players.First(x => x.Id == newPlayer.Id);
        }
        
        [GraphQLMutation]
        public Expression<Func<LeagueDbContext, Player>> DeletePlayer(LeagueDbContext context,DeleteItem args)
        {
            var playerToRemove = context.Players.First(x => x.Id == args.Id);
            context.Remove(playerToRemove);
            context.SaveChanges();
            return ctx => playerToRemove;
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Player>> UpdatePlayer(LeagueDbContext context, UpdatePlayer args)
        {
            var playerToUpdate = context.Players.First(x => x.Id == args.Id);
            playerToUpdate.Name = args.Name;
            playerToUpdate.SettlementId = args.SettlementId;
            if(args.Links.Count > 0)
            {
                var media = context.Medias.First(x => x.Url == args.Links[0]);
                playerToUpdate.MediaId = media.Id;
            }

            context.Players.Update(playerToUpdate);
            context.SaveChanges();

            return ctx => playerToUpdate;
        }
    }
}