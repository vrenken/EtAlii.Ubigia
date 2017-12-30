namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Authorization;

    //[RequiresAuthenticationToken]
    [Authorize]
    public partial class EntryController : WebApiController
    {
        private readonly IEntryRepository _items;

        public EntryController(IEntryRepository items)
        {
            _items = items;
        }
    }
}
