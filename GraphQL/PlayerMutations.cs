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

            if(!string.IsNullOrWhiteSpace(args.Name))
                playerToUpdate.Name = args.Name;

            if(!string.IsNullOrWhiteSpace(args.Link))
            {
                context.Remove(playerToUpdate.Media);

                playerToUpdate.Media.Url = args.Link;
            }

            context.Players.Update(playerToUpdate);
            context.SaveChanges();

            return ctx => playerToUpdate;
        }
    }
}