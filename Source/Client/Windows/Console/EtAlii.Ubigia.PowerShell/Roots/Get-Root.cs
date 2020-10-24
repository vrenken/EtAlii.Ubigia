﻿namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Get, Nouns.Root, DefaultParameterSetName = "byRootName")]
    public class GetRoot : RootTargetingCmdlet<Root>
    {
        protected override Task<Root> ProcessTask()
        {
            return Task.FromResult(TargetRoot);
        } 
    }
}