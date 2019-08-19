using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class PlayerGraphType : ObjectGraphType<Player>
    {
        public PlayerGraphType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}