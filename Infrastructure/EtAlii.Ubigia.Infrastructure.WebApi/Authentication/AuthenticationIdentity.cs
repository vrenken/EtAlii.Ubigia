namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System.Security.Principal;

    internal class AuthenticationIdentity : GenericIdentity
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