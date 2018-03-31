namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Linq;

    public class SpaceConnectionConfiguration : ISpaceConnectionConfiguration
    {
        public ISpaceTransport Transport { get; private set; }

        public Uri Address { get; private set; }

        public string AccountName { get; private set; }

        public string Password { get; private set; }

        public string Space { get; private set; }

        public ISpaceConnectionExtension[] Extensions { get; private set; }

        public SpaceConnectionConfiguration()
        {
            Extensions = new ISpaceConnectionExtension[0];
        }

        public ISpaceConnectionConfiguration Use(ISpaceConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public ISpaceConnectionConfiguration Use(ISpaceTransport transport)
        {
            if (transport == null)
            {
                throw new ArgumentException(nameof(transport));
            }
            if (Transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this SpaceConnectionConfiguration");
            }

            Transport = transport;
            return this;
        }

        public ISpaceConnectionConfiguration Use(Uri address)
        {
			if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this SpaceConnectionConfiguration");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        public ISpaceConnectionConfiguration Use(string accountName, string space, string password)
        {
            if (String.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this SpaceConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password));
            //}
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this SpaceConnectionConfiguration");
            }
            if (String.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException(nameof(space));
            }
            if (Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this SpaceConnectionConfiguration");
            }

            AccountName = accountName;
            Password = password;
            Space = space;
            return this;
        }

    }
}