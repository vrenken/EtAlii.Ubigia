namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Accounts
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Components;

    public partial class Edit
    {
        private Account _account;
        private string _accountName;
        private string _accountPassword;

        [Inject] private IInfrastructure Infrastructure { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter, SupplyParameterFromQuery] public Guid Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            _account = await Infrastructure.Accounts.Get(Id).ConfigureAwait(false);

            _accountName = _account.Name;
            _accountPassword = _account.Password;
        }

        private void OnCancelPressed()
        {
            NavigationManager.NavigateTo("/Accounts/");
        }

        private async Task OnSavePressed()
        {
            _account.Name = _accountName;
            _account.Password = _accountPassword;

            await Infrastructure.Accounts.Update(Id, _account).ConfigureAwait(false);
            NavigationManager.NavigateTo("/Accounts/");
        }

        private async Task OnDeletePressed()
        {
            await Infrastructure.Accounts.Remove(Id).ConfigureAwait(false);
            NavigationManager.NavigateTo("/Accounts/");
        }
    }
}
