using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace Gazin.Portal.GraphQL.Configurations
{
    public class PortalSchema : Schema
    {
        public PortalSchema(IServiceProvider services)
            : base(services)
        {
            Query = services.GetRequiredService<PortalQuery>();
            Mutation = services.GetRequiredService<PortalMutation>();
        }
    }
}