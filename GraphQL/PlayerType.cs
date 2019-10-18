using GraphQL.Types;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.GraphQL
{
    public class PlayerType : ObjectGraphType<Player>
    {
        public PlayerType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}