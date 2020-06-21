namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using EtAlii.Ubigia.PowerShell.Storages;

#pragma warning disable S110 // For the PowerShell part we don't worry about a too deep inheritance chain.
    [Cmdlet(VerbsCommon.Select, Nouns.Root, DefaultParameterSetName = "byRootName")]
    public class SelectRoot : RootTargetingCmdlet<Root>
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            RootCmdlet.Current = null;
        }
        protected override Task<Root> ProcessTask()
        {
            StorageCmdlet.CurrentStorage = TargetStorage;
            AccountCmdlet.Current = TargetAccount;
            SpaceCmdlet.Current = TargetSpace;
            RootCmdlet.Current = TargetRoot;
            return Task.FromResult(TargetRoot);
        } 
    }
}
