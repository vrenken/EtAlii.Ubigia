﻿namespace EtAlii.Ubigia.Api.Transport.Grpc
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
                Created = account.Created.ToDateTime(),
                Updated = account.Updated?.ToDateTime(),
                Name = account.Name,
                Password = account.Password,
                Roles = account.Roles.ToArray()
            };
        }

        public static WireProtocol.Account ToWire(this Account account)
        {
            // TODO: We should just return a account or crash. Returning null is a small security risk as it allows for probing.
            var result = account == null ? null : new WireProtocol.Account
            {
                Id = account.Id.ToWire(),
                Created = Timestamp.FromDateTime(account.Created),
                Updated = account.Updated.HasValue ? Timestamp.FromDateTime(account.Updated.Value) : null,
                Name = account.Name,
            };
            if (account?.Password != null)
            {
                result.Password = account.Password;
            }
            result?.Roles.AddRange(account.Roles.ToArray());
            return result;
        }

        public static IEnumerable<WireProtocol.Account> ToWire(this IEnumerable<Account> accounts)
        {
            return accounts.Select(s => s.ToWire());
        }
    }
}
