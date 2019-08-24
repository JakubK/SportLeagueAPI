using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL
{
    public class SettlementMutations
    {
        [GraphQLMutation]
        public Expression<Func<LeagueDbContext, Settlement>> AddSettlement(LeagueDbContext context, AddSettlement args)
        {
            var newSettlement = new Settlement();

            newSettlement.Name = args.Name;
            newSettlement.Description = args.Description;
            
            context.Settlements.Add(newSettlement);
            context.SaveChanges();

            return ctx => context.Settlements.First(x => x.Id == newSettlement.Id);
        }
    }
}