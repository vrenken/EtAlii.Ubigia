namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using System.Web.Http;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RequiresAuthenticationToken(Role.User, Role.System)]
    public partial class EntryController : ApiController
    {
        private readonly IEntryRepository _items;

        public EntryController(
            IEntryRepository items)
        {
            _items = items;
        }
    }
}
