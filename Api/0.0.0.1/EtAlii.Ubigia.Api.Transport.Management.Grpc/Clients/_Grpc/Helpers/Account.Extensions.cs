namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Linq;
    using Google.Protobuf.WellKnownTypes;

    public static class AccountExtension
    {
        public static Account ToLocal(this WireProtocol.Account account)
        {
            return new Account
            {
                Id = account.Id.ToLocal(),
                Created = account.Created.ToDateTime(),
                Updated = account.Updated.ToDateTime(),
                Name = account.Name,
                Password = account.Password,
                Roles = account.Roles.ToArray()
            };
        }

        public static WireProtocol.Account ToWire(this Account account)
        {
            var result = new WireProtocol.Account
            {
                Id = account.Id.ToWire(),
                Created = Timestamp.FromDateTime(account.Created),
                Updated = account.Updated.HasValue ? Timestamp.FromDateTime(account.Updated.Value) : null,
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
        
        public static IEnumerable<Account> ToLocal(this IEnumerable<WireProtocol.Account> accounts)
        {
            return accounts.Select(s => s.ToLocal());
        }
    }
}
