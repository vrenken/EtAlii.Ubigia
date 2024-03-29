﻿namespace EtAlii.Ubigia.Infrastructure.Portal.Admin.Accounts;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using Microsoft.AspNetCore.Components;

public partial class Add
{
    private Account _account;

    private AccountTemplate _template;

    [Inject] private IFunctionalContext FunctionalContext { get; set; }

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

    private async Task OnSavePressed()
    {
        await FunctionalContext.Accounts.Add(_account, _template).ConfigureAwait(false);
        NavigationManager.NavigateTo("/Accounts/");
    }
}
