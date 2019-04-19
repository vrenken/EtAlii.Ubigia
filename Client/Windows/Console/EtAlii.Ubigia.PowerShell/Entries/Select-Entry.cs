namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Select, Nouns.Entry)]
    public class SelectEntry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public bool Newest { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public bool Oldest { get; set; }

        protected override void BeginProcessing()
        {
            // Put any processing that should happen at the beginning here.
        }

        protected override void EndProcessing()
        {
            // Put any processing that should happen at the end here.
        }
    }
}
