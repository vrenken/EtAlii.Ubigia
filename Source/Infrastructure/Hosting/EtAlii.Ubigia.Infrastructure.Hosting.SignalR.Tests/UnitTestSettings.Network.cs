namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 15400, UnitTestConstants.NetworkPortRangeStart + 15599);
    }
}
