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
        public Expression<Func<LeagueDbContext, Settlement>> AddSettlement(LeagueDbContext context, CreateSettlement args)
        {
            var newSettlement = new Settlement();

            newSettlement.Name = args.Name;
            newSettlement.Description = args.Description;

            var media = context.Medias.FirstOrDefault(x => x.Url == args.Link);
            newSettlement.MediaId = media.Id;
            
            context.Settlements.Add(newSettlement);
            context.SaveChanges();

            return ctx => context.Settlements.First(x => x.Id == newSettlement.Id);
        }
    }
}