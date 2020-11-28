namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Principal;

    [SuppressMessage("Sonar Code Smell", "S4834:Controlling permissions is security-sensitive", Justification = "Safe to do so here.")]
    public class AuthenticationIdentity : GenericIdentity
    {
        public AuthenticationIdentity(string name, string password)
            : base(name, "Basic")
        {
            Password = password;
        }

        /// <summary>
        /// Basic Auth Password for custom authentication
        /// </summary>
        public string Password { get; set; }
    }
}