// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;

public static class AccountExtension
{
    public static Account ToLocal(this WireProtocol.Account account)
    {
        // We should just return a account or crash. Returning null is a small security risk as it allows for probing.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/81
        return account == null ? null : new Account
        {
            Id = account.Id.ToLocal(),
            Created = account.Created.ToDateTime(),
            Updated = account.Updated?.ToDateTime(),
            Name = account.Name,
            Password = account.Password,
            Roles = account.Roles.ToArray()
        };
    }

    public static WireProtocol.Account ToWire(this Account account)
    {
        // We should just return a account or crash. Returning null is a small security risk as it allows for probing.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/81
        if (account == null)
        {
            return null;
        }

        var result = new WireProtocol.Account
        {
            Id = account.Id.ToWire(),
            Created = Timestamp.FromDateTime(account.Created),
            Updated = account.Updated.HasValue ? Timestamp.FromDateTime(account.Updated.Value) : null,
            Name = account.Name,
        };
        if (account.Password != null)
        {
            result.Password = account.Password;
        }
        result.Roles.AddRange(account.Roles.ToArray());
        return result;
    }

    public static IEnumerable<WireProtocol.Account> ToWire(this IEnumerable<Account> accounts)
    {
        return accounts.Select(s => s.ToWire());
    }
}
