using GraphQL.Types;

namespace Gazin.Portal.GraphQL.Configurations
{
    public class PortalMutation : ObjectGraphType
    {
        public PortalMutation()
        {
            Name = "Mutation";
        }
    }
}