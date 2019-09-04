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
    public class EventMutations
    {
        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Event>> AddEvent(LeagueDbContext context, CreateEvent args)
        {
            var newEvent = new Event();

            newEvent.Name = args.Name;
            newEvent.Date = args.Date;
            newEvent.Description = args.Description;

            newEvent.Medias = context.Medias.Where(x => args.Links.Contains(x.Url)).ToArray();
            
            context.Events.Add(newEvent);
            context.SaveChanges();

            return ctx => newEvent;
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Event>> DeleteEvent(LeagueDbContext context, DeleteEvent args)
        {
            var eventToRemove = context.Events.Include(x => x.Scores).Include(x => x.Medias).Where(x => x.Id == args.Id).First();
            context.Remove(eventToRemove);
            context.SaveChanges();
            return ctx => eventToRemove;
        }

        public Expression<Func<LeagueDbContext,Event>> UpdateEvent(LeagueDbContext context, UpdateEvent args)
        {
            var eventToUpdate = context.Events.Include(x => x.Medias).First(x => x.Id == args.Id);

            if(!string.IsNullOrWhiteSpace(args.Name))
                eventToUpdate.Name = args.Name;

            if(!string.IsNullOrWhiteSpace(args.Description))
                eventToUpdate.Description = args.Description;

            if(args.Date != null)
                eventToUpdate.Date = args.Date;
            
            if(args.Links.Length != 0)
            {
                //delete old ones and add new ones
                foreach(var media in eventToUpdate.Medias)
                    context.Remove(media);

                eventToUpdate.Medias = context.Medias.Where(x => args.Links.Contains(x.Url)).ToArray();
            }

            context.Events.Update(eventToUpdate);
            context.SaveChanges();

            return ctx => eventToUpdate;
        }
    }
}