namespace EtAlii.Ubigia.Api.Functional
{
    using System.Security.Claims;

    public class UserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
