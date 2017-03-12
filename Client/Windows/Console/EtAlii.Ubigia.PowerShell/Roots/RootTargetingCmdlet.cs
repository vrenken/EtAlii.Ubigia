namespace EtAlii.Ubigia.PowerShell.Roots
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class RootTargetingCmdlet : SpaceTargetingCmdlet, IRootInfoProvider
    {
        [Parameter(Mandatory = false, Position = 70, ParameterSetName = "byRoot", HelpMessage = "The root on which the action should be applied.")]
        public Root Root { get; set; }

        [Parameter(Mandatory = false, Position = 70, ParameterSetName = "byRootName", HelpMessage = "The name of the root on which the action should be applied.")]
        public string RootName { get; set; }

        [Parameter(Mandatory = false, Position = 70, ParameterSetName = "byRootId", HelpMessage = "The ID of the root on which the action should be applied.")]
        public Guid RootId { get; set; }

        public Root TargetRoot { get { return _targetRoot; } private set { _targetRoot = value; } }
        private Root _targetRoot;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            var task = Task.Run(async () =>
            {
                TargetRoot = await PowerShellClient.Current.RootResolver.Get(this, AccountCmdlet.Current, SpaceCmdlet.Current, RootCmdlet.Current);
            });
            task.Wait();

            if (TargetRoot == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoRoot, ErrorCategory.InvalidData, null));
            }
            WriteDebug(String.Format("Using root [{0}]", TargetRoot != null ? TargetRoot.Name : "NONE"));
        }
    }
}
