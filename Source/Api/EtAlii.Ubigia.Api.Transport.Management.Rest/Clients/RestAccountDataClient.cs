// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Rest;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Rest;

internal class RestAccountDataClient : RestClientBase, IAccountDataClient
{
    public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
    {
        var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountTemplate, template.Name);
        var account = new Account
        {
            Name = accountName,
            Password = accountPassword,
        };

        account = await Connection.Client.Post(address, account).ConfigureAwait(false);
        return account;
    }

    public async Task Remove(Guid accountId)
    {
        var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountId, accountId.ToString());
        await Connection.Client.Delete(address).ConfigureAwait(false);
    }

    public async Task<Account> Change(Guid accountId, string accountName, string accountPassword)
    {
        var account = new Account
        {
            Id = accountId,
            Name = accountName,
            Password = accountPassword,
        };

        var changeAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountId, accountId.ToString());
        account = await Connection.Client.Put(changeAddress, account).ConfigureAwait(false);
        return account;
    }

    public async Task<Account> Change(Account account)
    {
        var changeAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountId, account.Id.ToString());
        account = await Connection.Client.Put(changeAddress, account).ConfigureAwait(false);
        return account;
    }

    public async Task<Account> Get(string accountName)
    {
        var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountName, accountName);
        var account = await Connection.Client.Get<Account>(address).ConfigureAwait(false);
        return account;
    }

    public async Task<Account> Get(Guid accountId)
    {
        var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountId, accountId.ToString());
        var account = await Connection.Client.Get<Account>(address).ConfigureAwait(false);
        return account;
    }

    public async IAsyncEnumerable<Account> GetAll()
    {
        var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Accounts);
        var result = await Connection.Client.Get<IEnumerable<Account>>(address).ConfigureAwait(false);
        foreach (var item in result)
        {
            yield return item;
        }
    }
}
