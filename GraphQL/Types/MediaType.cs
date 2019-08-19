using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class MediaType : ObjectGraphType<Media>
    {
        public MediaType()
        {
            Field(x => x.Url);
            Field(x => x.OwnerId);
        }
    }
}