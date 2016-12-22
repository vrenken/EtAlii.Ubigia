﻿
namespace EtAlii.Servus.PowerShell.Spaces
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.PowerShell.Accounts;
    using EtAlii.Servus.PowerShell.Roots;
    using EtAlii.Servus.PowerShell.Storages;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using Api.Fabric;
    using EtAlii.Servus.Api.Transport;

    [Cmdlet(VerbsCommon.Select, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class Select_Space : SpaceTargetingCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            SpaceCmdlet.Current = null;
            RootCmdlet.Current = null;
        }
        protected override void ProcessRecord()
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
                    IDataConnection connection = null;
                    var task = Task.Run(async () =>
                    {
                        connection = await PowerShellClient.Current.ManagementConnection.OpenSpace(TargetSpace.AccountId, TargetSpace.Id);
                    });
                    task.Wait();
                    var fabricContextConfiguration = new FabricContextConfiguration()
                        .Use(connection);
                    PowerShellClient.Current.Fabric = new FabricContextFactory().Create(fabricContextConfiguration);
                    space = PowerShellClient.Current.Fabric.Connection.Space;
                    WriteDebug(String.Format("Using space '{0}' at {1}", space.Name, TargetStorage.Address));
                }

            }
            catch (Exception e)
            {
                StorageCmdlet.Current = null;
                ThrowTerminatingError(new ErrorRecord(e, ErrorId.AuthenticationFailed, ErrorCategory.AuthenticationError, TargetStorage.Address));
            }

            WriteDebug(String.Format("Selecting space [{0}]", space != null ? space.Name : "NONE"));

            StorageCmdlet.Current = TargetStorage;
            AccountCmdlet.Current = TargetAccount;
            SpaceCmdlet.Current = space;
            WriteObject(space);
        } 

    }
}
