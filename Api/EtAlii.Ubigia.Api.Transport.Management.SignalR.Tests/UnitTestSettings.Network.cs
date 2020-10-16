#pragma warning disable CS0436    
namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 14000, UnitTestConstants.NetworkPortRangeStart + 14199);
    }
}