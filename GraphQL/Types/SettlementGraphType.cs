using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class SettlementGraphType : ObjectGraphType<Settlement>
    {
        public SettlementGraphType()
        {
            Field(x => x.Name);
            Field(x => x.Description);
        }
    }
}