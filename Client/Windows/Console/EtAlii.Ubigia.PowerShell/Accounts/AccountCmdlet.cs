namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public class AccountCmdlet : CmdletBase
    {
        public static Account Current { get; set; }
    }
}
