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

            if(args.Links.Count > 0)
            {
                newNews.MediaId = context.Medias.FirstOrDefault(x => x.Url == args.Links[0]).Id;
            }
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

            newsToUpdate.Name = args.Name;
            newsToUpdate.Description = args.Description;
            newsToUpdate.Date = args.Date;

            if(args.Links.Count > 0)
            {
                var media = context.Medias.First(x => x.Url == args.Links[0]);
                newsToUpdate.MediaId = media.Id;            
            }

            context.Newses.Update(newsToUpdate);
            context.SaveChanges();

            return ctx => newsToUpdate;
        }
    }
}