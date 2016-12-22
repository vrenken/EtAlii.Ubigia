namespace EtAlii.Servus.PowerShell.Accounts
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    public class AccountCmdlet : CmdletBase
    {
        public static Account Current { get; set; }
    }
}
