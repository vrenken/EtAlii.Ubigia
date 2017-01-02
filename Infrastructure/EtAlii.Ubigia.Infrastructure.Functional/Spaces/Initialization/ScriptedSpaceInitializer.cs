namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;

    internal class ScriptedSpaceInitializer : ISpaceInitializer
    {
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        public ScriptedSpaceInitializer(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public void Initialize(Space space, SpaceTemplate template)
        {
            var task = Task.Run(async () =>
            {
                var systemConnection = _systemConnectionCreationProxy.Request();
                var managementConnection = await systemConnection.OpenManagementConnection();
                var spaceConnection = await managementConnection.OpenSpace(space);
                var dataContext = new DataContextFactory().Create(spaceConnection);

                var rootsToCreate = template.RootsToCreate;

                foreach (var rootToCreate in rootsToCreate)
                {
                    var scope = new ScriptScope();
                    var createScript = dataContext.Scripts.Parse($"root:{rootToCreate} <= Object");
                    var processingResult = await dataContext.Scripts.Process(createScript.Script, scope);
                    await processingResult.Output.LastOrDefaultAsync();
                }
            });
            task.Wait();

        }
    }
}