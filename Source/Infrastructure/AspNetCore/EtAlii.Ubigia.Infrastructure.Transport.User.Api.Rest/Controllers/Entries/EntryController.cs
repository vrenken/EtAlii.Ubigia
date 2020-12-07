﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeDataUri.Entry)]
    public partial class EntryController : RestController
    {
        private readonly IEntryRepository _items;

        public EntryController(IEntryRepository items)
        {
            _items = items;
        }
    }
}
