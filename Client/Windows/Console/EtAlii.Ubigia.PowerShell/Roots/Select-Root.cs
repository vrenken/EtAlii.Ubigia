namespace EtAlii.Ubigia.PowerShell.Roots
{
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using EtAlii.Ubigia.PowerShell.Storages;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Select, Nouns.Root, DefaultParameterSetName = "byRootName")]
    public class SelectRoot : RootTargetingCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            RootCmdlet.Current = null;
        }
        protected override void ProcessRecord()
        {
            StorageCmdlet.Current = TargetStorage;
            AccountCmdlet.Current = TargetAccount;
            SpaceCmdlet.Current = TargetSpace;
            RootCmdlet.Current = TargetRoot;
            WriteObject(TargetRoot);
        } 
    }
}
