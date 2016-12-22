namespace EtAlii.Servus.PowerShell
{
    using EtAlii.Servus.Api;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Nouns.Entry)]
    public class Get_Entry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public Identifier identifier;

        [Parameter(Mandatory = false, Position = 1)]
        public bool newest;

        [Parameter(Mandatory = false, Position = 2)]
        public bool oldest;

        protected override void BeginProcessing()
        {
        }

        protected override void EndProcessing()
        {
        }
    }
}
