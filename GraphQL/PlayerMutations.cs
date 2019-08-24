using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL
{
    public class PlayerMutations
    {
        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Player>> AddPlayer(LeagueDbContext context, AddPlayer args)
        {
            var newPlayer = new Player();
            newPlayer.Name = args.Name;
            newPlayer.Settlement = context.Settlements.First(x => x.Name == args.SettlementName);

            context.Players.Add(newPlayer);
            context.SaveChanges();

            return ctx => context.Players.First(x => x.Id == newPlayer.Id);
        }
    }
}