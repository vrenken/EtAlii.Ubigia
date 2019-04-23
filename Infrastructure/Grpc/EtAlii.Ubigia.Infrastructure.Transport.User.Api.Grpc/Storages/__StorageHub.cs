//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
//[
//    using System
//    using System.Collections.Generic
//    using EtAlii.Ubigia.Api
//    using EtAlii.Ubigia.Infrastructure.Functional

//    public class StorageHub : HubBase
//    [
//        private readonly IStorageRepository _items

//        public StorageHub(
//            IStorageRepository items,
//            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
//            : base(authenticationTokenVerifier)
//        [
//            _items = items
//        ]
//        public Storage GetLocal(string local)
//        [
//            Storage response
//            try
//            [
//                response = _items.GetLocal()
//            ]
//            catch [Exception ex]
//            [
//                throw new InvalidOperationException("Unable to serve a Storage GET client request", e)
//            ]
//            return response
//        ]
//    ]
//]