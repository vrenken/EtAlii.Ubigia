namespace EtAlii.Ubigia.Infrastructure.Transport.Setup.Portal
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Components;

    public partial class Dashboard
    {
        private AccountRow[] _accounts;
        private StorageRow[] _storages;

        [Inject] private IFunctionalContext FunctionalContext { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var accountIndex = 1;
            _accounts = await FunctionalContext.Accounts
                .GetAll()
                .Take(3)
                .SelectAwait(async a => new AccountRow
                {
                    Index = accountIndex++,
                    Name = a.Name,
                    Id = a.Id,
                    Spaces = (await FunctionalContext.Spaces.GetAll(a.Id).ToArrayAsync().ConfigureAwait(false)).Length
                })
                .ToArrayAsync()
                .ConfigureAwait(false);

            var storageIndex = 1;
            _storages = await FunctionalContext.Storages
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

        private void OnManageAccounts()
        {
            NavigationManager.NavigateTo("/Accounts/");
        }

        private void OnEditAccount(AccountRow account)
        {
            NavigationManager.NavigateTo($"/Accounts/Edit?Id={account.Id}");
        }
    }
}
