#pragma warning disable CS0436    
namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 12800, UnitTestConstants.NetworkPortRangeStart + 12999);
    }
}