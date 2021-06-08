namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 19000, UnitTestConstants.NetworkPortRangeStart + 19499);
    }
}
