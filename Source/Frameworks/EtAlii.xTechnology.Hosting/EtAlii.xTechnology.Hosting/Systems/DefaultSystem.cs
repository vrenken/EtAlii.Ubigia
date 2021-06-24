// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public class DefaultSystem : SystemBase
    {
        private static int _defaultSystemCounter;

        #pragma warning disable S2696 // Pretty sure this counter won't cause any weird threading issues.
        protected override Status CreateInitialStatus() => new($"System {++_defaultSystemCounter}");
        #pragma warning restore S2696
    }
}
