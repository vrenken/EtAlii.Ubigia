namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System.Linq;
    using System.Threading.Tasks;

    public partial class Dashboard
    {
        private AccountRow[] _accounts;
        private StorageRow[] _storages;

        protected override async Task OnInitializedAsync()
        {
            var accountIndex = 1;
            _accounts = await _infrastructure.Accounts
                .GetAll()
                .Take(3)
                .SelectAwait(async a => new AccountRow
                {
                    Index = accountIndex++,
                    Name = a.Name,
                    Id = a.Id,
                    Spaces = (await _infrastructure.Spaces.GetAll(a.Id).ToArrayAsync().ConfigureAwait(false)).Length
                })
                .ToArrayAsync()
                .ConfigureAwait(false);

            var storageIndex = 1;
            _storages = await _infrastructure.Storages
                .GetAll()
                .Take(3)
                .Select(s => new StorageRow
                {
                    Index = storageIndex++,
                    Name = s.Name,
                    Id = s.Id
                })
                .ToArrayAsync()
                .ConfigureAwait(false);
        }
    }
}
