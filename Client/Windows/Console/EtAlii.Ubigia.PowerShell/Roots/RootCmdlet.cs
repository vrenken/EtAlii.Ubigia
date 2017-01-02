namespace EtAlii.Ubigia.PowerShell.Roots
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;

    public class RootCmdlet : CmdletBase
    {
        public static Root Current { get; set; }
    }
}
