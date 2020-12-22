namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Threading;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptProcessor), typeof(LoggingScriptProcessor));

                container.RegisterDecorator(typeof(IAbsolutePathSubjectProcessor), typeof(LoggingAbsolutePathSubjectProcessor));
                container.RegisterDecorator(typeof(IRelativePathSubjectProcessor), typeof(LoggingRelativePathSubjectProcessor));
                container.RegisterDecorator(typeof(IRootedPathSubjectProcessor), typeof(LoggingRootedPathSubjectProcessor));

                container.RegisterDecorator(typeof(IRootPathProcessor), typeof(LoggingRootPathProcessor));

                container.Register<IContextCorrelator, ContextCorrelator>();
            }
        }
    }
}
