using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class MediaGraphType : ObjectGraphType<Media>
    {
        public MediaGraphType()
        {
            Field(x => x.Url);
            Field(x => x.OwnerId);
        }
    }
}