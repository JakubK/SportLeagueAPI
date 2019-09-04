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

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,News>> DeleteNews(LeagueDbContext context, DeleteItem args)
        {
            var newsToRemove = context.Newses.First(x => x.Id == args.Id);
            context.Remove(newsToRemove);
            context.SaveChanges();
            return ctx => newsToRemove;
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,News>> UpdateNews(LeagueDbContext context, UpdateNews args)
        {
            var newsToUpdate = context.Newses.First(x => x.Id == args.Id);

            if(!string.IsNullOrWhiteSpace(args.Name))
                newsToUpdate.Name = args.Name;

            if(!string.IsNullOrWhiteSpace(args.Description))
                newsToUpdate.Description = args.Description;

            if(args.Date != null)
                newsToUpdate.Date = args.Date;

            if(!string.IsNullOrWhiteSpace(args.Link))
            {
                context.Remove(newsToUpdate.Media);

                newsToUpdate.Media.Url = args.Link;
            }

            context.Newses.Update(newsToUpdate);
            context.SaveChanges();

            return ctx => newsToUpdate;
        }
    }
}