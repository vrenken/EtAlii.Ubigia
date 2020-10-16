#pragma warning disable CS0436    
namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 13000, UnitTestConstants.NetworkPortRangeStart + 13199);
    }
}