namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.Threading;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsLinqQueryContextExtension : ILinqQueryContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsLinqQueryContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            if (_diagnostics.EnableLogging)
            {
                container.RegisterDecorator(typeof(INodeQueryExecutor), typeof(LoggingNodeQueryExecutor));
                container.RegisterDecorator(typeof(IRootQueryExecutor), typeof(LoggingRootQueryExecutor));
                container.Register<IContextCorrelator, ContextCorrelator>();
            }
        }
    }
}
