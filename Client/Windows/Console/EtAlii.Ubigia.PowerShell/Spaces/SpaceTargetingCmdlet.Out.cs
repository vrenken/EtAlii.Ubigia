namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Accounts;

    public abstract class SpaceTargetingCmdlet<TOut> : AccountTargetingCmdlet<TOut>, ISpaceInfoProvider
        where TOut: class
    {
        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "bySpace", HelpMessage = "The space on which the action should be applied.")]
        public Space Space { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "bySpaceName", HelpMessage = "The name of the space on which the action should be applied.")]
        public string SpaceName { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "bySpaceId", HelpMessage = "The ID of the space on which the action should be applied.")]
        public Guid SpaceId { get; set; }

        public Space TargetSpace { get; private set; }

        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask();

            TargetSpace = await PowerShellClient.Current.SpaceResolver.Get(this, SpaceCmdlet.Current, AccountCmdlet.Current);

            if (TargetSpace == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoSpace, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using space [{(TargetSpace != null ? TargetSpace.Name : "NONE")}]")
        }
    }
}
