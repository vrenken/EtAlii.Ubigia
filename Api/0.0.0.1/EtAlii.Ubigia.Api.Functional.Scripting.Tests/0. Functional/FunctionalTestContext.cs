namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class FunctionalTestContext : IFunctionalTestContext
    {
        private readonly ILogicalTestContext _logical;
        private readonly IDiagnosticsConfiguration _diagnostics;

        public FunctionalTestContext(
            ILogicalTestContext logical, 
            IDiagnosticsConfiguration diagnostics)
        {
            _logical = logical;
            _diagnostics = diagnostics;
        }

        public async Task<IDataContext> CreateFunctionalContext(bool openOnCreation)
        {
            var logicalContext = await _logical.CreateLogicalContext(openOnCreation);
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(logicalContext);
            return new DataContextFactory().Create(configuration);
        }

        #region start/stop
        
        public async Task Start()
        {
            await _logical.Start();
        }

        public async Task Stop()
        {
            await _logical.Stop();
        }

        #endregion start/stop
    }
}