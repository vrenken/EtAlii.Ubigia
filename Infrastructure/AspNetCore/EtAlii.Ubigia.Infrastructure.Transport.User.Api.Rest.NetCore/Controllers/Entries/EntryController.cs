namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.NetCore
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    [Route(RelativeUri.User.Api.Entry)]
    public partial class EntryController : RestController
    {
        private readonly IEntryRepository _items;

        public EntryController(IEntryRepository items)
        {
            _items = items;
        }
    }
}
