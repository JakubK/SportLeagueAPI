using System;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.Models;
using SportLeagueAPI.Services;

namespace SportLeagueAPI.GraphQL
{
    public class AuthMutations
    {   
        [GraphQLMutation]
        public JsonWebToken SignIn(LeagueDbContext dbContext, IServiceProvider _serviceProvider)
        {
            return null;
        }
    }
}