namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Collections;
    using System.Collections.Generic;

    public class ConfigurationFiles : IEnumerable<object[]>
    {
        public const string Systems1Variant1 = "System 1/settings_variant_1.json"; 
        public const string Systems1Variant2 = "System 1/settings_variant_2.json"; 
        public const string Systems2VariantWebApi = "System 2/settings_variant_webapi.json"; 
        public const string Systems2VariantGrpc = "System 2/settings_variant_grpc.json"; 
        public const string Systems2VariantSignalR = "System 2/settings_variant_signalr.json"; 
        
        private readonly IReadOnlyList<object[]> _data = new List<object[]>
        {
            new object[] {Systems1Variant1},
            new object[] {Systems1Variant2},
            new object[] {Systems2VariantGrpc},
            new object[] {Systems2VariantWebApi},
            new object[] {Systems2VariantSignalR}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
