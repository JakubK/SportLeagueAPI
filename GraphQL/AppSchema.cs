using EntityGraphQL.Schema;
using SportLeagueAPI.Context;

namespace SportLeagueAPI.GraphQL
{
    public class AppSchema
    {
        public static MappedSchemaProvider<LeagueDbContext> MakeSchema()
        {
            var schema = SchemaBuilder.FromObject<LeagueDbContext>();

            schema.AddMutationFrom(new EventMutations());

            return schema;
        }
    }
}