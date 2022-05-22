namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Accounts
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Components;

    public partial class Add
    {
        private Account _account;

        private AccountTemplate _template;

        [Inject] private IInfrastructure Infrastructure { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            _account = new Account();
            _template = AccountTemplate.User;
        }

        private void OnCancelPressed()
        {
            NavigationManager.NavigateTo("/Accounts/");
        }

        private Task OnSavePressed()
        {
            Infrastructure.Accounts.Add(_account, _template);
            NavigationManager.NavigateTo("/Accounts/");

            return Task.CompletedTask;
        }
    }
}
