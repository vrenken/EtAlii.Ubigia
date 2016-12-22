namespace EtAlii.Servus.PowerShell.Roots
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;

    public class RootCmdlet : CmdletBase
    {
        public static Root Current { get; set; }
    }
}
