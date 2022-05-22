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

        protected override void OnParametersSet()
        {
            _account = Infrastructure.Accounts.Get(Id);

            _accountName = _account.Name;
            _accountPassword = _account.Password;
        }

        private void OnCancelPressed()
        {
            NavigationManager.NavigateTo("/Accounts/");
        }

        private Task OnSavePressed()
        {
            _account.Name = _accountName;
            _account.Password = _accountPassword;

            Infrastructure.Accounts.Update(Id, _account);
            NavigationManager.NavigateTo("/Accounts/");

            return Task.CompletedTask;
        }
    }
}
