namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System;
    using global::Owin;
    using Owin;

    public class ProfilingWebApiComponentManager : IWebApiComponentManager
    {
        private readonly IWebApiComponentManager _componentManager;
        //private readonly IProfiler _profiler;

        private const string _startDurationCounter = "ComponentManager.Start.Duration";
        private const string _startCountCounter = "ComponentManager.Start.Count";
        private const string _stopDurationCounter = "ComponentManager.Stop.Duration";
        private const string _stopCountCounter = "ComponentManager.Stop.Count";

        public ProfilingWebApiComponentManager(IWebApiComponentManager componentManager
            //, IProfiler profiler
            )
        {
            _componentManager = componentManager;
            //_profiler = profiler;

            //profiler.Register(_startDurationCounter, SamplingType.RawCount, "Milliseconds", "Start the ComponentManager", "The time it takes for the Start method to execute");
            //profiler.Register(_startCountCounter, SamplingType.IncrementalCount, "Count", "Number of times the ComponentManager is started", "The number of times the Start method has executed");
            //profiler.Register(_stopDurationCounter, SamplingType.RawCount, "Milliseconds", "Stop the ComponentManager", "The time it takes for the Stop method to execute");
            //profiler.Register(_stopCountCounter, SamplingType.IncrementalCount, "Count", "Number of times the ComponentManager is stopped", "The number of times the Stop method has executed");
        }

        public void Start(object iAppBuilder)
        {
            var application = (IAppBuilder)iAppBuilder;
            var start = Environment.TickCount;
            _componentManager.Start(iAppBuilder);
            //_profiler.WriteSample(_startDurationCounter, Environment.TickCount - start);
            //_profiler.WriteSample(_startCountCounter, 1d);
        }

        public void Stop()
        {
            var start = Environment.TickCount;
            _componentManager.Stop();
            //_profiler.WriteSample(_stopDurationCounter, Environment.TickCount - start);
            //_profiler.WriteSample(_stopCountCounter, 1d);
        }
    }
}