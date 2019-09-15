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

            newSettlement.MediaId = context.Medias.FirstOrDefault(x => x.Url == args.Link).Id;
            
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

            if(!string.IsNullOrWhiteSpace(args.Name))
                settlementToUpdate.Name = args.Name;

            if(!string.IsNullOrWhiteSpace(args.Description))
                settlementToUpdate.Description = args.Description;

            if(!string.IsNullOrWhiteSpace(args.Link))
            {
                context.Remove(settlementToUpdate.Media);
                settlementToUpdate.Media.Url = args.Link;
            }

            context.Settlements.Update(settlementToUpdate);
            context.SaveChanges();

            return ctx => settlementToUpdate;
        }
    }
}