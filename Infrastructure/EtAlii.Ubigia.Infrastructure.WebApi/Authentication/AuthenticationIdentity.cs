namespace EtAlii.Ubigia.Infrastructure
{
    using System.Security.Principal;

    internal class AuthenticationIdentity : GenericIdentity
    {
        public AuthenticationIdentity(string name, string password)
            : base(name, "Basic")
        {
            this.Password = password;
        }

        /// <summary>
        /// Basic Auth Password for custom authentication
        /// </summary>
        public string Password { get; set; }
    }
}