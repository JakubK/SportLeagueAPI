using System;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Models;
using SportLeagueAPI.Services;

namespace SportLeagueAPI.GraphQL
{
    public class PlayerMutations
    {

        [GraphQLMutation]
        public Expression<Func<LeagueDbContext,Player>> AddPlayer(LeagueDbContext context, AddPlayer args)
        {
            var newPlayer = new Player();
            newPlayer.Name = args.Name;

            Console.Error.WriteLine("We got this far");


            // var newMedia = new Media();
            // newMedia.Url = _mediaUploader.UploadMedia(args.File);

            // context.Medias.Add(newMedia);

            // newPlayer.MediaId = newMedia.Id;

            context.Players.Add(newPlayer);
            context.SaveChanges();

            return ctx => context.Players.First(x => x.Id == newPlayer.Id);
        }
    }
}