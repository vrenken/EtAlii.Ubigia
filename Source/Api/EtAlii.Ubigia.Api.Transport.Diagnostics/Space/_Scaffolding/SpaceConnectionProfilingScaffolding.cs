﻿namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionProfilingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateProfilerFactory());
            container.Register(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                //container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection))
            }
        }
    }
}