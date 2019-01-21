namespace EtAlii.Ubigia.PowerShell.Entries
{
    using EtAlii.Ubigia.Api;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Nouns.Entry)]
    public class GetEntry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public Identifier Identifier { get; set; }

        [Parameter(Mandatory = false, Position = 1)]
        public bool Newest { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public bool Oldest { get; set; }

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
