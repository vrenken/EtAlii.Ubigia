// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class AccountContext : StorageClientContextBase<IAccountDataClient, IAccountNotificationClient>, IAccountContext
    {
        public AccountContext(
            IAccountNotificationClient notifications, 
            IAccountDataClient data) 
            : base(notifications, data)
        {
        }

        public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate accountTemplate)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return await Data.Add(accountName, accountPassword, accountTemplate).ConfigureAwait(false);
        }

        public async Task Remove(Guid accountId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            await Data.Remove(accountId).ConfigureAwait(false);
        }

        public async Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Change(accountId, accountName, accountPassword).ConfigureAwait(false);
        }

        public async Task<Account> Change(Account account)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Change(account).ConfigureAwait(false);
        }

        public async Task<Account> Get(string accountName)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(accountName).ConfigureAwait(false);
        }

        public async Task<Account> Get(Guid accountId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(accountId).ConfigureAwait(false);
        }

        public IAsyncEnumerable<Account> GetAll()
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return Data.GetAll();
        }
    }
}
