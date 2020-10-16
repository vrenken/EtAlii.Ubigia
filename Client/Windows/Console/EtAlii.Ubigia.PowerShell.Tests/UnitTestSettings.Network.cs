namespace EtAlii.Ubigia.PowerShell.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 15400, UnitTestConstants.NetworkPortRangeStart + 15599);
    }
}