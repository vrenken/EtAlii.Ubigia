// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingPropertiesManager : IPropertiesManager
    {
        private readonly IPropertiesManager _decoree;
        private readonly IProfiler _profiler;

        public ProfilingPropertiesManager(IPropertiesManager decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Properties);
        }

        public async Task<PropertyDictionary> Get(Identifier identifier, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Get: " + identifier.ToTimeString());
            profile.Identifier = identifier.ToString();

            var result = await _decoree.Get(identifier, scope).ConfigureAwait(false);
            profile.Result = result;

            _profiler.End(profile);

            return result;
        }

        public async Task Set(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Set: " + identifier.ToTimeString());
            profile.Identifier = identifier.ToString();
            profile.Properties = properties;

            await _decoree.Set(identifier, properties, scope).ConfigureAwait(false);

            _profiler.End(profile);
        }

        public async Task<bool> HasProperties(Identifier identifier, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Checking for properties: " + identifier.ToTimeString());
            profile.Identifier = identifier.ToString();

            var result = await _decoree.HasProperties(identifier, scope).ConfigureAwait(false);

            profile.Result = result;
            profile.Action = "Checking for properties: " + identifier.ToTimeString() + (!result ? "" : " - AVAILABLE");
            _profiler.End(profile);

            return result;
        }
    }
}
