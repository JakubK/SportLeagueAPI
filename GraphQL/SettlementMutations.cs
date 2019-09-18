using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
using Microsoft.EntityFrameworkCore;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL
{
    public class SettlementMutations
    {
        [GraphQLMutation]
        public Expression<Func<LeagueDbContext, Settlement>> AddSettlement(LeagueDbContext context, CreateSettlement args)
        {
            var newSettlement = new Settlement();

            newSettlement.Name = args.Name;
            newSettlement.Description = args.Description;

            if(args.Links.Count > 0)
                newSettlement.MediaId = context.Medias.FirstOrDefault(x => x.Url == args.Links[0]).Id;
            
            context.Settlements.Add(newSettlement);
            context.SaveChanges();

            return ctx => context.Settlements.First(x => x.Id == newSettlement.Id);
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Settlement>> DeleteSettlement(LeagueDbContext context, DeleteItem args)
        {
            var settlementToRemove = context.Settlements.Include(x => x.Players).First(x => x.Id == args.Id);
            context.Remove(settlementToRemove);
            context.SaveChanges();
            return ctx => settlementToRemove;
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Settlement>> UpdateSettlement(LeagueDbContext context, UpdateSettlement args)
        {
            var settlementToUpdate = context.Settlements.First(x => x.Id == args.Id);

            settlementToUpdate.Name = args.Name;
            settlementToUpdate.Description = args.Description;

            if(args.Links.Count > 0)
            {
                var media = context.Medias.First(x => x.Url == args.Links[0]);
                settlementToUpdate.MediaId = media.Id;
            }

            context.Settlements.Update(settlementToUpdate);
            context.SaveChanges();

            return ctx => settlementToUpdate;
        }
    }
}