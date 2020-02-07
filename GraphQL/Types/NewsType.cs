using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL.Types
{
    public class NewsType : ObjectGraphType<News>
    {
        public NewsType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Date);
            Field<StringGraphType>("media", resolve: x => x.Source.Media.Url);
        }
    }
}