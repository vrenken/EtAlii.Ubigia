namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using EtAlii.Ubigia.PowerShell.Accounts;
    using System;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Add, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class Add_Space : AccountTargetingCmdlet, IAccountInfoProvider
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the space that should be added.")]
        public string SpaceName { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The template that should be added.")]
        public string Template { get; set; }


        protected override void ProcessRecord()
        {
            Space space = null;

            WriteDebug(String.Format("Adding space {0}", SpaceName));

            var task = Task.Run(async () =>
            {
                var template = SpaceTemplate.All.Single(t => t.Name == Template);
                space = await PowerShellClient.Current.ManagementConnection.Spaces.Add(TargetAccount.Id, SpaceName, template);
            });
            task.Wait();

            WriteObject(space);
        }
    }
}
