using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class MediaGraphType : ObjectGraphType<Media>
    {
        public MediaGraphType()
        {
            Field(x => x.Id);
            Field(x => x.Url);
        }
    }
}