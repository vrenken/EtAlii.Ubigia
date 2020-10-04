namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Select, Nouns.Entry)]
    public class SelectEntry : TaskCmdlet<Entry>
    {
        [Parameter(Mandatory = false, Position = 1)]
        public bool Newest { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public bool Oldest { get; set; }

        protected override Task<Entry> ProcessTask()
        {
            // Put any processing that should happen at the end here.
            return Task.FromResult((Entry)null);
        }
    }
}
