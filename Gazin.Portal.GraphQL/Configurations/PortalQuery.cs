using GraphQL;
using GraphQL.Types;

namespace Gazin.Portal.GraphQL.Configurations
{
    public class PortalQuery : ObjectGraphType<object>
    {
        public PortalQuery()
        {
            Name = "Query";

            Field<StringGraphType>(
                name: "hello_word",
                resolve: context => $"Hello {context.GetArgument<string>("name")}! I'm fine and you?"
            );
        }
    }
}