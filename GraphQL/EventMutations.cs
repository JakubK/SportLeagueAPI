using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
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

            var medias = context.Medias.Where(x => args.Links.Contains(x.Url));
            newEvent.Medias = medias.ToArray();

            context.Events.Add(newEvent);
            context.SaveChanges();

            return ctx => context.Events.First(x => x.Id == newEvent.Id);
        }
    }
}