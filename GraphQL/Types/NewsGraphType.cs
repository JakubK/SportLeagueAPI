using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class NewsGraphType : ObjectGraphType<News>
    {
        public NewsGraphType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Date);
        }
    }
}