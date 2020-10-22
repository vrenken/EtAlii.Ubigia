namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    public static class UnitTestSettings
    {
        // For our hosting tests we need a bigger range.
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 16800, UnitTestConstants.NetworkPortRangeStart + 17299);
    }
}