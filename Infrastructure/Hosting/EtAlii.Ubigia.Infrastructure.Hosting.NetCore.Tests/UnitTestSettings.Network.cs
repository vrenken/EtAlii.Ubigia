namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 15200, UnitTestConstants.NetworkPortRangeStart + 15399);
    }
}