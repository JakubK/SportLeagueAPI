using GraphQL.Types;
using SportLeagueAPI.Models;
using SportLeagueAPI.Repositories;

namespace SportLeagueAPI.GraphQL.Types
{
    public class EventGraphType : ObjectGraphType<Event>
    {
        public EventGraphType(MediaRepository mediaRepository)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Date);
            Field<ListGraphType<MediaGraphType>>("medias", 
            resolve: context => 
            {
                return mediaRepository.GetMultipleByOwnerId(context.Source.Id);
            });
        }
    }
}