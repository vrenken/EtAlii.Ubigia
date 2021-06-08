namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    public static class UnitTestSettings
    {
        // For our hosting tests we need a bigger range.
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 17000, UnitTestConstants.NetworkPortRangeStart + 17499);
    }
}
