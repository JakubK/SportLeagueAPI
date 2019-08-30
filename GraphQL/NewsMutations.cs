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

            newNews.MediaId = context.Medias.FirstOrDefault(x => x.Url == args.Link).Id;

            context.Newses.Add(newNews);
            context.SaveChanges();

            return ctx => context.Newses.First(x => x.Id == newNews.Id);
        }
    }
}