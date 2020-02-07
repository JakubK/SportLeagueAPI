using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL.Types
{
    public class MediaType : ObjectGraphType<Media>
    {
        public MediaType()
        {
            Field(x => x.Id);
            Field("media", x => x.Url);
        }
    }
}