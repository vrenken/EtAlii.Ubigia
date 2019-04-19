//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
//[
//    using System
//    using EtAlii.Ubigia.Api

//    public partial class EntryHub : HubBase
//    [
//        // Update Item by id
//        public Entry Put(Entry entry)
//        [
//            Entry response
//            try
//            [
//                // Store the entry.
//                response = _items.Store(entry)

//                // Send the stord event.
//                SignalStored(entry.Id)
//            }
//            catch (Exception e)
//            [
//                throw new InvalidOperationException("Unable to serve a Entry PUT client request", e)
//            }
//            return response
//        }
//    }
//}
