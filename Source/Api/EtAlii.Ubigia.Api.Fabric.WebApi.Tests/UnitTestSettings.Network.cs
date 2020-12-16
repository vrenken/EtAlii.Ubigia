
namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 13400, UnitTestConstants.NetworkPortRangeStart + 13599);
    }
}
