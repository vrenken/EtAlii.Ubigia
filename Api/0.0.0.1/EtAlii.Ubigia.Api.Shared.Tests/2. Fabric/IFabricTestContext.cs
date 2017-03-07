namespace EtAlii.Ubigia.Api.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public interface IFabricTestContext
    {
        Task<IFabricContext> CreateFabricContext(bool openOnCreation);
        Task<Tuple<IEditableEntry, string[]>> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, int depth);//, out string[] hierarchy);

        Task Start();
        Task Stop();
    }
}