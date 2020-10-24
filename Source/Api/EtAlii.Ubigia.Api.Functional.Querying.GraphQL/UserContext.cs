namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Security.Claims;

    public class UserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
