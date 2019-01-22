namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public interface IFabricTestContext
    {
        Task<IFabricContext> CreateFabricContext(bool openOnCreation);
        Task<Tuple<IEditableEntry, string[]>> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, int depth);

        Task Start();
        Task Stop();
    }
}