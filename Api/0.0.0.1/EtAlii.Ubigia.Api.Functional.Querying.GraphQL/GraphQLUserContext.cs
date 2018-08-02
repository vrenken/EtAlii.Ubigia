using System.Security.Claims;

namespace EtAlii.Ubigia.Api.Functional
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
