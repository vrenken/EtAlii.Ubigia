// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Linq;
    using Google.Protobuf.WellKnownTypes;

    public static class AccountExtension
    {
        public static Account ToLocal(this WireProtocol.Account account)
        {
            // TODO: We should just return a account or crash. Returning null is a small security risk as it allows for probing.
            return account == null ? null : new Account
            {
                Id = account.Id.ToLocal(),
                Created = account.Created.ToDateTime().ToUniversalTime(),
                Updated = account.Updated?.ToDateTime().ToUniversalTime(),
                Name = account.Name,
                Password = account.Password,
                Roles = account.Roles.ToArray()
            };
        }

        public static IEnumerable<Account> ToLocal(this IEnumerable<WireProtocol.Account> accounts)
        {
            return accounts.Select(s => s.ToLocal());
        }

        public static WireProtocol.Account ToWire(this Account account)
        {
            // TODO: We should just return a account or crash. Returning null is a small security risk as it allows for probing.
            if (account == null)
            {
                return null;
            }
            var result = new WireProtocol.Account
            {
                Id = account.Id.ToWire(),
                Created = Timestamp.FromDateTime(account.Created.ToUniversalTime()),
                Updated = account.Updated.HasValue ? Timestamp.FromDateTime(account.Updated.Value.ToUniversalTime()) : null,
                Name = account.Name,
                Password = account.Password,
            };
            result.Roles.AddRange(account.Roles.ToArray());
            return result;
        }

        public static IEnumerable<WireProtocol.Account> ToWire(this IEnumerable<Account> accounts)
        {
            return accounts.Select(s => s.ToWire());
        }
    }
}
