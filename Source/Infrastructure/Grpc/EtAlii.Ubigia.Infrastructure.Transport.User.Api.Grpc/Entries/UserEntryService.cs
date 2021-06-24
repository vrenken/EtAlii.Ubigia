// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public partial class UserEntryService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.EntryGrpcService.EntryGrpcServiceBase, IUserEntryService
    {
        private readonly IEntryRepository _items;
        //private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier

        public UserEntryService(
            IEntryRepository items
            //ISimpleAuthenticationTokenVerifier authenticationTokenVerifier
            )
        {
            _items = items;
            //_authenticationTokenVerifier = authenticationTokenVerifier
        }
    }
}
