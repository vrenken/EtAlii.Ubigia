namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Spaces : CollectionBase<StorageConnection>
    {
        internal Spaces(StorageConnection connection)
            : base(connection)
        {
        }

        public const string RelativePath = "retrieval/space";

        public Space Add(Guid accountId, string spaceName)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath);
            
            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };

            space = Infrastructure.Post<Space>(address, space);
            return space;
        }

        public void Remove(Guid spaceId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.SpaceId, spaceId.ToString());
            Infrastructure.Delete(address);
        }

        public Space Change(Guid spaceId, string spaceName)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var space = new Space
            {
                Id = spaceId,
//                AccountId = accountId,
                Name = spaceName,
            };

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.SpaceId, spaceId.ToString());
            space = Infrastructure.Put(address, space);
            return space;
        }

        public Space Get(Guid accountId, string spaceName)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.AccountId, accountId.ToString(), UriParameter.SpaceName, spaceName);
            var space = Infrastructure.Get<Space>(address);
            return space;
        }

        public Space Get(Guid spaceId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.SpaceId, spaceId.ToString());
            var space = Infrastructure.Get<Space>(address);
            return space;
        }

        public IEnumerable<Space> GetAll(Guid accountId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.AccountId, accountId.ToString());
            var spaces = Infrastructure.Get<IEnumerable<Space>>(address);
            return spaces;
        }

    }
}
