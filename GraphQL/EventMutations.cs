using System;
using System.Collections.Generic;
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
            newEvent.Season = args.Season;
            
            if(args.Links != null)
                newEvent.Medias = context.Medias.Where(x => args.Links.Contains(x.Url)).ToArray();
                 
            context.Events.Add(newEvent);

            foreach(var item in args.Scores)
            {
                context.Scores.Add(new Score()
                {
                        PlayerId = item.PlayerId,
                        Points = item.Points,
                        EventId = newEvent.Id
                });
            }
            
            context.SaveChanges();

            return ctx => newEvent;
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Event>> DeleteEvent(LeagueDbContext context, DeleteItem args)
        {
            var eventToRemove = context.Events.Include(x => x.Scores).Include(x => x.Medias).Where(x => x.Id == args.Id).First();
            context.Remove(eventToRemove);
            context.SaveChanges();
            return ctx => eventToRemove;
        }

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Event>> UpdateEvent(LeagueDbContext context, UpdateEvent args)
        {
            var eventToUpdate = context.Events.Include(x => x.Medias).Include(x => x.Scores).First(x => x.Id == args.Id);
            
            eventToUpdate.Name = args.Name;
            eventToUpdate.Description = args.Description;
            eventToUpdate.Date = args.Date;
            eventToUpdate.Season = args.Season;

            if(args.Links != null)
            {
                //delete old ones and add new ones
                context.RemoveRange(eventToUpdate.Medias);
                var linkMedias = context.Medias.Where(x => args.Links.Contains(x.Url));
                foreach(var linkMedia in linkMedias)
                {
                    linkMedia.EventId = eventToUpdate.Id;
                }
                context.UpdateRange(linkMedias);
            }
                      
            context.Scores.RemoveRange(eventToUpdate.Scores);
            foreach(var score in args.Scores)
            {
                context.Scores.Add(new Score{
                    PlayerId = score.PlayerId,
                    Points = score.Points,
                    EventId = eventToUpdate.Id
                });
            }

            context.Events.Update(eventToUpdate);
            context.SaveChanges();

            return ctx => eventToUpdate;
        }
    }
}