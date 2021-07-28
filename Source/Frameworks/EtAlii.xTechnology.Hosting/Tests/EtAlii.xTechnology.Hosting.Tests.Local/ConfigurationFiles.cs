// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Collections;
    using System.Collections.Generic;

    public class ConfigurationFiles : IEnumerable<object[]>
    {
        public const string HostSettingsSystems1Variant1 = "System 1/HostSettingsVariant1.json";
        public const string HostSettingsSystems1Variant2 = "System 1/HostSettingsVariant2.json";
        public const string HostSettingsSystems2VariantRest = "System 2/HostSettingsRest.json";
        public const string HostSettingsSystems2VariantGrpc = "System 2/HostSettingsGrpc.json";
        public const string HostSettingsSystems2VariantSignalR = "System 2/HostSettingsSignalr.json";

        public const string ClientSettings = "ClientSettings.json";

        private readonly IReadOnlyList<object[]> _data = new List<object[]>
        {
            new object[] {HostSettingsSystems1Variant1},
            new object[] {HostSettingsSystems1Variant2},
            new object[] {HostSettingsSystems2VariantGrpc},
            new object[] {HostSettingsSystems2VariantRest},
            new object[] {HostSettingsSystems2VariantSignalR}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
