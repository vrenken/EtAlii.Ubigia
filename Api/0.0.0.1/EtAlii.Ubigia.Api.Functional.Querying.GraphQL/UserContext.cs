namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Security.Claims;

    public class UserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
