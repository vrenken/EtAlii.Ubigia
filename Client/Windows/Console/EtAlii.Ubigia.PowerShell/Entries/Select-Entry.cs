namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Select, Nouns.Entry)]
    public class SelectEntry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public bool Newest;

        [Parameter(Mandatory = false, Position = 2)]
        public bool Oldest;

        protected override void BeginProcessing()
        {
            // Do stuff before processing has started.
        }

        protected override void EndProcessing()
        {
            // Do stuff after processing has started.
        }
    }
}
