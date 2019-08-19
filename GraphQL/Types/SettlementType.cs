using GraphQL.Types;
using SportLeagueAPI.Models;

namespace SportLeagueAPI.GraphQL.Types
{
    public class SettlementType : ObjectGraphType<Settlement>
    {
        public SettlementType()
        {
            Field(x => x.Name);
            Field(x => x.Description);
        }
    }
}