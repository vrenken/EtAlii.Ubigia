﻿namespace EtAlii.Ubigia.PowerShell.Roots
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsData.Update, Nouns.Root, DefaultParameterSetName = "byRoot")]
    public class UpdateSpace : SpaceTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "byRoot")]
        public Root Root { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (Root == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoRoot, ErrorCategory.InvalidData, null));
            }
        }

        protected override void ProcessRecord()
        {
            var task = Task.Run(async () =>
            {
                await PowerShellClient.Current.Fabric.Roots.Change(Root.Id, Root.Name);
            });
            task.Wait();

            var verboseDescription = $"Root '{Root.Name}' has been updated.";
            WriteVerbose(verboseDescription);
        }
    }
}
