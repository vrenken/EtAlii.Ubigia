namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Roots;
    using EtAlii.Ubigia.PowerShell.Storages;
    using EtAlii.xTechnology.Diagnostics;

    [Cmdlet(VerbsCommon.Select, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class SelectSpace : SpaceTargetingCmdlet<Space>
    {
        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask().ConfigureAwait(false);

            SpaceCmdlet.Current = null;
            RootCmdlet.Current = null;
        }
        protected override async Task<Space> ProcessTask()
        {
            var space = (Space) null;

            try
            {
                if (PowerShellClient.Current.Fabric != null && PowerShellClient.Current.Fabric.Connection.IsConnected)
                {
                    PowerShellClient.Current.Fabric.Dispose();
                }
                PowerShellClient.Current.Fabric = null;

                if (TargetSpace != null)
                {
                    var connection = await PowerShellClient.Current.ManagementConnection.OpenSpace(TargetSpace.AccountId, TargetSpace.Id).ConfigureAwait(false);
                    var fabricContextConfiguration = new FabricContextConfiguration()
                        .Use(connection)
                        .Use(DiagnosticsConfiguration.Default);
                    PowerShellClient.Current.Fabric = new FabricContextFactory().Create(fabricContextConfiguration);
                    space = PowerShellClient.Current.Fabric.Connection.Space;
                    //WriteDebug($"Using space '{space.Name}' at {TargetStorage.Address}")
                }

            }
            catch (Exception e)
            {
                StorageCmdlet.CurrentStorage = null;
                ThrowTerminatingError(new ErrorRecord(e, ErrorId.AuthenticationFailed, ErrorCategory.AuthenticationError, TargetStorage.Address));
            }

            //WriteDebug($"Selecting space [{(space != null ? space.Name : "NONE")}]")

            StorageCmdlet.CurrentStorage = TargetStorage;
            AccountCmdlet.Current = TargetAccount;
            SpaceCmdlet.Current = space;
            return space;
        } 

    }
}
