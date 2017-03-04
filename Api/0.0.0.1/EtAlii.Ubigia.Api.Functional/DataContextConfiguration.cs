namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public class DataContextConfiguration : IDataContextConfiguration
    {
        public ILogicalContext LogicalContext => _logicalContext;
        private ILogicalContext _logicalContext;

        public IDataContextExtension[] Extensions => _extensions;
        private IDataContextExtension[] _extensions;

        public IFunctionHandlersProvider FunctionHandlersProvider => _functionHandlersProvider;
        private IFunctionHandlersProvider _functionHandlersProvider;

        public IRootHandlerMappersProvider RootHandlerMappersProvider => _rootHandlerMappersProvider;
        private IRootHandlerMappersProvider _rootHandlerMappersProvider;

        public DataContextConfiguration()
        {
            _extensions = new IDataContextExtension[0];
            _functionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
            _rootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
        }

        public IDataContextConfiguration Use(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
            return this;
        }

        public IDataContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, _functionHandlersProvider.FunctionHandlers);
            return this;
        }

        public IDataContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }

        public IDataContextConfiguration Use(IDataContextExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IDataContextConfiguration Use(IDataConnection dataConnection)
        {
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(dataConnection);
            var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabricContext);
            var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

            return Use(logicalContext);
        }
    }
}