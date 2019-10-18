using GraphQL;
using GraphQL.Types;

namespace SportLeagueAPI.GraphQL
{
    public class LeagueSchema : Schema
    {
        public LeagueSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<LeagueQuery>();
        }
    }
}