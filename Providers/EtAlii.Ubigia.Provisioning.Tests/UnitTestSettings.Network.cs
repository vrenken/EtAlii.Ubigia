#pragma warning disable CS0436    
namespace EtAlii.Ubigia.Provisioning.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 15600, UnitTestConstants.NetworkPortRangeStart + 15799);
    }
}