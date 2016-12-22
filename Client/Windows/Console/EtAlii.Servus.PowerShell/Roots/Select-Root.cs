namespace EtAlii.Servus.PowerShell.Roots
{
    using EtAlii.Servus.PowerShell.Accounts;
    using EtAlii.Servus.PowerShell.Spaces;
    using EtAlii.Servus.PowerShell.Storages;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Select, Nouns.Root, DefaultParameterSetName = "byRootName")]
    public class Select_Root : RootTargetingCmdlet
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
