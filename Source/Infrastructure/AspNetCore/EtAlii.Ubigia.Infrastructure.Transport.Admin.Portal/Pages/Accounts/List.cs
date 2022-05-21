namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Components;

    public partial class List
    {
        private AccountRow[] _accounts;

        [Inject] private IInfrastructure Infrastructure { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var accountIndex = 1;
            _accounts = await Infrastructure.Accounts
                .GetAll()
                .Take(3)
                .SelectAwait(async a => new AccountRow
                {
                    Index = accountIndex++,
                    Name = a.Name,
                    Id = a.Id,
                    Spaces = (await Infrastructure.Spaces.GetAll(a.Id).ToArrayAsync().ConfigureAwait(false)).Length
                })
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        private void OnEditAccount(AccountRow account)
        {
            NavigationManager.NavigateTo($"/Accounts/Edit?Id={account.Id}");
        }

        private void OnAddAccount()
        {
            NavigationManager.NavigateTo("/Accounts/Add");
        }
    }
}
