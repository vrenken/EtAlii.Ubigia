namespace EtAlii.Ubigia.Api.Fabric
{
    internal class EntryCacheContextProvider : IEntryCacheContextProvider
    {
        public IEntryContext Context { get; }

        public EntryCacheContextProvider(IEntryContext context)
        {
            Context = context;
        }
    }
}
