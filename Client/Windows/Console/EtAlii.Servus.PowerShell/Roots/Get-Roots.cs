﻿namespace EtAlii.Servus.PowerShell.Roots
{
    using EtAlii.Servus.PowerShell.Spaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;

    [Cmdlet(VerbsCommon.Get, Nouns.Roots, DefaultParameterSetName = "BySpaceName")]
    [Description("Gets the roots for the specified space")]
    public class Get_Roots : SpaceTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            IEnumerable<Root> roots = null;

            WriteDebug(String.Format("Getting roots for [{0}]", TargetSpace.Name));

            var task = Task.Run(async () =>
            {
                roots = await PowerShellClient.Current.Fabric.Roots.GetAll();
            });
            task.Wait();

            WriteObject(roots);
        }
    }
}
