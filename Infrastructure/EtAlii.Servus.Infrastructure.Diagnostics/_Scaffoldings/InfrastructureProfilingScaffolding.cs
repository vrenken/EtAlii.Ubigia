namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class InfrastructureProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public InfrastructureProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<IProfilerFactory>(() => _diagnostics.CreateProfilerFactory(), Lifestyle.Singleton);
            container.Register<IProfiler>(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()), Lifestyle.Singleton);

            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IEntryRepository), typeof(ProfilingEntryRepositoryDecorator), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(IIdentifierRepository), typeof(ProfilingIdentifierRepositoryDecorator), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(IStorageRepository), typeof(ProfilingStorageRepositoryDecorator), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(IAccountRepository), typeof(ProfilingAccountRepositoryDecorator), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(ISpaceRepository), typeof(ProfilingSpaceRepositoryDecorator), Lifestyle.Singleton);
            }
        }
    }
}