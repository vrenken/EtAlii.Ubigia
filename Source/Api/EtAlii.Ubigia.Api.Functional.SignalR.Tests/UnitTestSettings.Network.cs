
namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 12000, UnitTestConstants.NetworkPortRangeStart + 12199);
    }
}
