using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL
{
    public class NewsMutations
    {
        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,News>> AddNews(LeagueDbContext context, CreateNews args)
        {
            var newNews = new News();

            newNews.Name = args.Name;
            newNews.Date = args.Date;
            newNews.Description = args.Description;

            context.Newses.Add(newNews);
            context.SaveChanges();

            return ctx => context.Newses.First(x => x.Id == newNews.Id);
        }
    }
}